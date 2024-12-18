namespace MySolution;

class Program
{
    static void Main(string[] args)
    {
        DbRepository dbRepository = new DbRepository("Data Source=school.db;Version=3;");
        dbRepository.TaskC3();
    }
}