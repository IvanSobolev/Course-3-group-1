using System.Data.SQLite;

namespace mySolution;

public class DbRepository(string contectionString)
{
    private readonly string _conectionString = contectionString;
    public void CreateTable()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      CREATE TABLE IF NOT EXISTS Contacts (
                                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        Name TEXT NOT NULL,
                                                        PhoneNumber TEXT NOT NULL)
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void AddContact(Contact contact)
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string insertQuery = "INSERT INTO Contacts (Name, PhoneNumber) VALUES (@Name, @PhoneNumber)";

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", contact.Name);
                command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void DeleteContactById(int id)
    {
        using (var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM Contacts WHERE Id = @Id";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
    
    public Contact? GetContactByName(string name)
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string GetByNameQuery = "SELECT * FROM Contacts WHERE Name LIKE @Name";

            using (var command = new SQLiteCommand(GetByNameQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", "%" + name + "%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Contact(Convert.ToInt32(reader["Id"]), (string)reader["Name"], (string)reader["PhoneNumber"]);
                    }
                }
            }
        }

        return null;
    }
    
    public Contact? GetContactById(int id)
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string GetByNameQuery = "SELECT * FROM Contacts WHERE Id LIKE @Id";

            using (var command = new SQLiteCommand(GetByNameQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", "%" + id + "%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Contact(Convert.ToInt32(reader["Id"]), (string)reader["Name"], (string)reader["PhoneNumber"]);
                    }
                }
            }
        }

        return null;
    }
    
    public List<Contact> GetAllContacts()
    {
        List<Contact> result = new List<Contact>();
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string GetByNameQuery = "SELECT * FROM Contacts";

            using (var command = new SQLiteCommand(GetByNameQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Contact(Convert.ToInt32(reader["Id"]), (string)reader["Name"], (string)reader["PhoneNumber"]));
                    }
                }
            }
        }

        return result;
    }
    
}