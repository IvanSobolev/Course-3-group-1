namespace MySolution;

class Program
{
    static void Main(string[] args)
    {
        DbRepository dbRepository = new DbRepository("Data Source=rent.db;Version=3;");
        dbRepository.GetDataPracticeC4();
    }
}