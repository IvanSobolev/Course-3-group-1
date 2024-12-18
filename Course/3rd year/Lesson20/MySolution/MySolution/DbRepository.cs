namespace MySolution;

using System.Data.SQLite;

public class DbRepository (string conntectionString)
{
    private readonly string _conectionString = conntectionString;

    public void TaskB1()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           UPDATE teacher
                           SET name = 'Михаил Михайлович'
                           WHERE id = 2;
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void TaskB2()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           UPDATE schedule
                           SET lesson_number = 5
                           WHERE tsc_id = (
                               SELECT tsc.id
                               FROM teachers_subjects_classes AS tsc
                               JOIN subject AS s ON tsc.subject_id = s.id
                               WHERE tsc.class_id = 4 AND s.name = 'Математика'
                           );
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void TaskB3()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           DELETE FROM student
                           WHERE id = 3;
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void TaskB4()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           DELETE FROM schedule
                           WHERE tsc_id = (
                               SELECT tsc.id
                               FROM teachers_subjects_classes AS tsc
                               WHERE tsc.class_id = 2 AND tsc.subject_id = 2
                           );
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void TaskB5()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           UPDATE student
                           SET class_id = 1
                           WHERE id = 5;
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void TaskC1()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                                      SELECT s.name, c.course
                                      FROM student AS s, class AS c
                                      WHERE s.class_id = c.id;
                                      """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["name"] + "\t" + reader["course"]);
                    }
                }
            }
        }
    }
    
    public void TaskC2()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                                      SELECT s.id AS id, 
                                             s.date AS date, 
                                             s.lesson_number AS number, 
                                             t.name AS tname, 
                                             sub.name AS subname, 
                                             c.course AS c
                                      FROM schedule AS s
                                      JOIN teachers_subjects_classes AS tsc ON s.tsc_id = tsc.id
                                      JOIN teacher As t ON tsc.teacher_id = t.id
                                      JOIN subject AS sub ON tsc.subject_id = sub.id
                                      JOIN class AS c ON tsc.class_id = c.id
                                      WHERE c.id = 4
                                      ORDER BY s.date, s.lesson_number;
                                      """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["id"] + "\t" + reader["date"] + "\t" + reader["number"] + "\t" + reader["tname"] + "\t" + reader["subname"] + "\t" + reader["c"]);
                    }
                }
            }
        }
    }
    
    public void TaskC3()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                          SELECT 
                              s.id AS id,
                              s.name AS name,
                              c.id AS cid,
                              c.course AS cname,
                              COUNT(c.id) AS ccount
                          FROM student AS s
                          JOIN class AS c ON s.class_id = c.id
                          GROUP BY s.id, c.id
                          ORDER BY s.id;
                          """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["id"] + "\t" + reader["name"] + "\t" + reader["cid"] + "\t" + reader["cname"] + "\t" + reader["ccount"]);
                    }
                }
            }
        }
    }
    
    public void CreateDB()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           CREATE TABLE IF NOT EXISTS teacher (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               name TEXT NOT NULL
                           );

                           CREATE TABLE IF NOT EXISTS class (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               course TEXT NOT NULL
                           );
                           CREATE TABLE IF NOT EXISTS student (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               name TEXT NOT NULL,
                               age INTEGER NOT NULL,
                               class_id INTEGER NOT NULL,
                               FOREIGN KEY (class_id) REFERENCES class(id)
                           );

                           CREATE TABLE IF NOT EXISTS subject (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               name TEXT NOT NULL
                           );

                           CREATE TABLE teachers_subjects_classes (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               teacher_id INTEGER NOT NULL,
                               subject_id INTEGER NOT NULL,
                               class_id INTEGER NOT NULL,
                               FOREIGN KEY (teacher_id) REFERENCES teacher (id) ON DELETE CASCADE,
                               FOREIGN KEY (subject_id) REFERENCES subject (id) ON DELETE CASCADE,
                               FOREIGN KEY (class_id) REFERENCES class (id) ON DELETE CASCADE
                           );

                           CREATE TABLE schedule (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               date DATETIME NOT NULL,
                               lesson_number INTEGER NOT NULL,
                               tsc_id INTEGER NOT NULL,
                               FOREIGN KEY (tsc_id) REFERENCES teachers_subjects_classes (id) ON DELETE CASCADE
                           );
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void InsertDB()
    {
        using(var connection = new SQLiteConnection(_conectionString))
        {
            connection.Open();

            string Query = """
                           INSERT INTO teacher (name) VALUES
                           ('Иван Иванов'), 
                           ('Мария Петрова'),
                           ('Алексей Сидоров'), 
                           ('Ольга Смирнова'),
                           ('Дмитрий Кузнецов'),
                           ('Елена Васильева'),
                           ('Артем Павлов'), 
                           ('Татьяна Романова'),
                           ('Сергей Орлов'), 
                           ('Анна Белова');

                           INSERT INTO class (course) VALUES
                           ('10A'), 
                           ('10B'),
                           ('10C'), 
                           ('11A'),
                           ('11B'),
                           ('9A'), 
                           ('9B'),
                           ('8A'),
                           ('8B'),
                           ('7A');

                           INSERT INTO subject (name) VALUES
                           ('Математика'), 
                           ('Физика'),
                           ('Русский язык'), 
                           ('Литература'),
                           ('История'),
                           ('География'), 
                           ('Информатика'),
                           ('Химия'),
                           ('Биология'),
                           ('Английский язык');

                           INSERT INTO teachers_subjects_classes (teacher_id, subject_id, class_id) VALUES
                           (1, 1, 1),
                           (2, 2, 2),
                           (3, 3, 3),
                           (4, 4, 4),
                           (5, 5, 5),
                           (6, 6, 6),
                           (7, 7, 7),
                           (8, 8, 8),
                           (9, 9, 9),
                           (10, 10, 10);

                           INSERT INTO student (name, age, class_id) VALUES
                           ('Александр', 16, 1),
                           ('Екатерина', 15, 2),
                           ('Николай', 17, 3),
                           ('Марина', 16, 4),
                           ('Владимир', 14, 5),
                           ('Дарья', 15, 6),
                           ('Павел', 17, 7),
                           ('Светлана', 16, 8),
                           ('Георгий', 15, 9),
                           ('Юлия', 14, 10);

                           INSERT INTO schedule (date, lesson_number, tsc_id) VALUES
                           ('2024-12-18', 1, 1),
                           ('2024-12-18', 2, 2),
                           ('2024-12-18', 3, 3),
                           ('2024-12-18', 4, 4),
                           ('2024-12-18', 5, 5),
                           ('2024-12-19', 1, 6),
                           ('2024-12-19', 2, 7),
                           ('2024-12-19', 3, 8),
                           ('2024-12-19', 4, 9),
                           ('2024-12-19', 5, 10);
                           """;

            using (var command = new SQLiteCommand(Query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}