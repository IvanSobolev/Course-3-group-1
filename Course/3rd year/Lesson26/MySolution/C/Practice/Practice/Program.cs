using System.Data.SQLite;

namespace Practice;

class Program
{
    static void Main(string[] args)
    {
        
    }

    static async Task ReserveAsync(int id)
    {
        int maxRetries = 3;
        int retries = 0;
        bool success = false;

        using (var conn = new SQLiteConnection("Data Source=sql.db; Version=3;"))
        {
            while (retries < maxRetries && !success)
            {
                try
                {
                    await conn.OpenAsync();
                    using (var transaction = conn.BeginTransaction())
                    {
                        var command = conn.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = "SELECT * FROM Seats WHERE seat_id = @id AND is_reserved = 0;";
                        command.Parameters.AddWithValue("@id", id);
                        var reader = await command.ExecuteReaderAsync();
                        
                        if (!reader.HasRows)
                        {
                            throw new Exception("Место занято.");
                        }
                        
                        reader.Close();

                        command.CommandText = "INSERT INTO Reservations (seat_id, reserved_at) VALUES (@id, CURRENT_TIMESTAMP);";
                        await command.ExecuteNonQueryAsync();

                        await LogAsync(conn, id, "SUCCESS", "SUCCESS");
                        
                        transaction.Commit();
                        success = true;
                    }
                }
                catch (SQLiteException ex)
                {
                    if (ex.ResultCode == SQLiteErrorCode.Busy || ex.ResultCode == SQLiteErrorCode.Locked)
                    {
                        retries++;
                        Console.WriteLine(
                            $"Попытка {retries} резервирования места {id} не удалась из-за блокировки: {ex.Message}");

                        // Логгирование неуспешной попытки
                        await LogAsync(conn, id,"FAILURE", ex.Message);

                        await Task.Delay(1000); // Задержка перед повторной попыткой
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при резервировании места {id}: {ex.Message}");
                        break;
                    }
                }
            }
        }
        
        static async Task LogAsync(SQLiteConnection conn, int id,string status, string msg)
        {
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText =
                        "INSERT INTO ReservationAttempts (seat_id, attempt_time, status, error) VALUES (@id, CURRENT_TIMESTAMP, @status, @mag);";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@msg", msg);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при логгировании: {ex.Message}");
            }
        }
    }
}