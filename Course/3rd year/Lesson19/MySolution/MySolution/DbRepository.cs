using System.Data.SQLite;
using MySolution.Model;

namespace MySolution;

public class DbRepository(string conntectionString)
{
    private readonly string _conectionString = conntectionString;
    
    public void CreateAllTable()
    {
        CreateUserTable();
        CreateRoomsTable();
        CreateReservationsTable();
        CreateReviewsTable();
    }
    
    public void InsertAllData()
    {
        InsertUsers();
        InsertRooms();
        InsertReservations();
        InsertReviews();
    }

    public void GetDataPracticeB1()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT * FROM Rooms
                                      WHERE HasTv = 1 AND HasInternet = 1;
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Id"] + "\t" +  reader["HomeType"]+ "\t" + reader["Address"] + "\t" + reader["HasTv"]+ "\t" + reader["HasInternet"] + "\t" + reader["HasKitchen"]+ "\t" + reader["HasAirCon"] + "\t" + reader["Price"]+ "\t" + reader["OwnerId"] + "\t" + reader["Latitube"]+ "\t" + reader["Lonitube"]);
                    }
                }
            }
        }
    }
    public void GetDataPracticeB2() //🍉🍉🍉🍉🍉🍉🍉🍉🍉🍉🍉🍉🍉🍉🍉
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT r.Id, r.StartDate, r.EndDate, r.Price, r.Total
                                      FROM Reservations r
                                      WHERE r.UserId = 1;
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Id"] + "\t" +  reader["StartDate"]+ "\t" + reader["EndDate"] + "\t" + reader["Price"]+ "\t" + reader["Total"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeB3()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT *
                                      FROM Users u
                                      WHERE u.EmailVerified = 0;
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Id"] + "\t" +  reader["Name"]+ "\t" + reader["Email"] + "\t" + reader["EmailVerified"]+ "\t" + reader["PasswordHash"] + "\t" + reader["PhoneNumber"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeB4( DateTime StartDate, DateTime EndDate)
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT *
                                      FROM Rooms r
                                      WHERE NOT EXISTS(
                                          SELECT 1 FROM Reservations res
                                          WHERE res.RoomId = r.Id AND ((res.StartDate <= @EndDate AND res.EndDate >= @StartDate))
                                      );
                                      """;
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.Parameters.AddWithValue("@StartDate", StartDate);
                command.Parameters.AddWithValue("@EndDate", EndDate);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Id"] + "\t" +  reader["HomeType"]+ "\t" + reader["Address"] + "\t" + reader["HasTv"]+ "\t" + reader["HasInternet"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeB5()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT AVG(Price) AS AvergagePrice
                                      FROM Rooms
                                      """;
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["AvergagePrice"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeC1(DateTime StartDate, DateTime EndDate)
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT SUM(Total) AS TotalRevenue
                                      FROM Reservations
                                      WHERE StartDate >= @StartDate AND EndDate <= @EndDate
                                      """;
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.Parameters.AddWithValue("@StartDate", StartDate);
                command.Parameters.AddWithValue("@EndDate", EndDate);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["TotalRevenue"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeC2(float StartPrice, float EndPrice)
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT *
                                      FROM Rooms
                                      WHERE Price BETWEEN @StartPrice AND @EndPrice
                                      """;
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.Parameters.AddWithValue("@StartPrice", StartPrice);
                command.Parameters.AddWithValue("@EndPrice", EndPrice);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Id"] + "\t" + reader["HomeType"]  + "\t" + reader["Price"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeC3()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT room.Id, room.Address, COUNT(review.Id) AS revCount, AVG(review.Raiting) AS averageRating
                                      FROM Rooms room
                                      LEFT JOIN Reservations res ON room.Id = res.RoomId
                                      LEFT JOIN Reviews review ON res.Id = review.ReservationId
                                      GROUP BY room.Id, room.Address
                                      """;
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Id"] + "\t" + reader["Address"] + "\t" + reader["revCount"]  + "\t" + reader["averageRating"]);
                    }
                }
            }
        }
    }
    
    public void GetDataPracticeC4()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      SELECT HomeType, MAX(Price) as hPrice
                                      FROM Rooms room
                                      GROUP BY HomeType
                                      """;
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["HomeType"] + "\t" + reader["hPrice"]);
                    }
                }
            }
        }
    }
    
    private void CreateUserTable()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      CREATE TABLE IF NOT EXISTS Users (
                                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        Name TEXT NOT NULL,
                                                        Email TEXT NOT NULL UNIQUE,
                                                        EmailVerified BOOLEAN NOT NULL DEFAULT 0,
                                                        PasswordHash TEXT NOT NULL,
                                                        PhoneNumber TEXT)
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    private void CreateRoomsTable()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      CREATE TABLE IF NOT EXISTS Rooms (
                                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        HomeType TEXT NOT NULL,
                                                        Address TEXT NOT NULL,
                                                        HasTv BOOLEAN NOT NULL DEFAULT 0,
                                                        HasInternet BOOLEAN NOT NULL DEFAULT 0,
                                                        HasKitchen BOOLEAN NOT NULL DEFAULT 0,
                                                        HasAirCon BOOLEAN NOT NULL DEFAULT 0,
                                                        Price REAL NOT NULL,
                                                        OwnerId INTEGER NOT NULL,
                                                        Latitube REAL,
                                                        Lonitube REAL,
                                                        FOREIGN KEY (OwnerId) REFERENCES Users(Id))
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    private void CreateReservationsTable()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      CREATE TABLE IF NOT EXISTS Reservations (
                                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        UserId INTEGER NOT NULL,
                                                        RoomId INTEGER NOT NULL,
                                                        StartDate DATE NOT NULL,
                                                        EndDate DATE NOT NULL,
                                                        Price REAL NOT NULL,
                                                        Total REAL NOT NULL,
                                                        FOREIGN KEY (UserId) REFERENCES Users(Id),
                                                        FOREIGN KEY (RoomId) REFERENCES Rooms(Id))
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    private void CreateReviewsTable()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string createTableQuery = """
                                      CREATE TABLE IF NOT EXISTS Reviews (
                                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        ReservationId INTEGER NOT NULL,
                                                        Raiting INTEGER NOT NULL CHECK (Raiting BETWEEN 1 AND 5),
                                                        FOREIGN KEY (ReservationId) REFERENCES Reservations(Id))
                                      """;

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    private void InsertUsers()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string insertQuery = """
                                   
                                 INSERT INTO Users (Name, Email, EmailVerified, PasswordHash, PhoneNumber) VALUES
                                    ('Ivan Sobolev', 'sobolev.ivan@example.com', 1, 'hashed_password_1', '123-4567'),
                                    ('Mongol_Kirill', 'Mongol.Kirill@example.com', 0, 'hashed_password_2', '123-8910'),
                                    ('Artemiy Lesnichow', 'Artemi.Lesnikow@example.com', 0, 'hashed_password_3', '234-1234'),
                                    ('Andrew_Ded', 'Andrew.Ded@example.com', 0, 'hashed_password_3', '234-5678'),
                                    ('Daria Petrovna', 'daria.petrovna@example.com', 1, 'hashed_password_4', '345-6789'),
                                    ('Svetlana Kuznetsova', 'svetlana.kuznetsova@example.com', 0, 'hashed_password_5', '456-7890'),
                                    ('Mikhail Ivanov', 'mikhail.ivanov@example.com', 1, 'hashed_password_6', '567-8901'),
                                    ('Elena Volkova', 'elena.volkova@example.com', 0, 'hashed_password_7', '678-9012'),
                                    ('Sergei Baranov', 'sergei.baranov@example.com', 1, 'hashed_password_8', '789-0123'),
                                    ('Natalia Morozova', 'natalia.morozova@example.com', 0, 'hashed_password_9', '890-1234');
                                 """;

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    private void InsertRooms()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string insertQuery = """
                                   INSERT INTO Rooms (HomeType, Address, HasTv, HasInternet, HasKitchen, HasAirCon, Price, OwnerId, Latitube, Lonitube) VALUES
                                    ('Apartment', '101 Elm Street', 1, 1, 1, 0, 150.00, 3, 36.1627, -86.7816),
                                    ('House', '202 Birch Lane', 1, 1, 1, 1, 250.00, 4, 39.7392, -104.9903),
                                    ('Studio', '303 Cedar Drive', 0, 1, 1, 0, 90.00, 5, 42.3601, -71.0589),
                                    ('Apartment', '404 Spruce Circle', 1, 1, 1, 1, 130.00, 2, 37.7749, -122.4194),
                                    ('House', '505 Redwood Avenue', 1, 1, 1, 1, 300.00, 6, 45.5017, -73.5673),
                                    ('Apartment', '606 Pinecrest Road', 1, 0, 0, 1, 110.00, 7, 47.6062, -122.3321),
                                    ('Studio', '707 Willow Way', 0, 1, 1, 0, 85.00, 8, 33.4484, -112.0740),
                                    ('House', '808 Oak Grove Street', 1, 1, 1, 1, 220.00, 9, 38.9072, -77.0373),
                                    ('Apartment', '909 Maple Avenue', 1, 1, 1, 0, 140.00, 10, 36.1699, -115.1398),
                                    ('Studio', '1010 Birch Boulevard', 0, 1, 0, 1, 95.00, 1, 34.0522, -118.2437);
                                 """;

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    private void InsertReviews()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string insertQuery = """
                                   INSERT INTO Reviews (ReservationId, Raiting) VALUES
                                    (1, 5),
                                    (2, 4),
                                    (3, 5),
                                    (4, 3),
                                    (5, 5),
                                    (6, 4),
                                    (7, 2),
                                    (8, 5),
                                    (9, 3),
                                    (10, 4);
                                 """;

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    private void InsertReservations()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string insertQuery = """
                                   INSERT INTO Reservations (UserId, RoomId, StartDate, EndDate, Price, Total) VALUES
                                 (3, 4, '2024-08-12', '2024-08-18', 150.00, 900.00),
                                 (4, 5, '2024-08-15', '2024-08-20', 250.00, 1250.00),
                                 (5, 6, '2024-08-18', '2024-08-25', 90.00, 630.00),
                                 (2, 1, '2024-08-20', '2024-08-27', 120.00, 840.00),
                                 (6, 7, '2024-08-22', '2024-08-30', 300.00, 2400.00),
                                 (7, 8, '2024-08-25', '2024-08-31', 110.00, 660.00),
                                 (8, 9, '2024-08-28', '2024-09-03', 85.00, 595.00),
                                 (9, 10, '2024-08-30', '2024-09-05', 220.00, 1320.00),
                                 (10, 2, '2024-09-01', '2024-09-07', 80.00, 480.00),
                                 (11, 3, '2024-09-05', '2024-09-10', 200.00, 1000.00);
                                 """;

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}