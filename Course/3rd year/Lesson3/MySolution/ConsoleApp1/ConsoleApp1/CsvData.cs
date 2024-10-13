using System.Globalization;

namespace ConsoleApp1;
using CsvHelper;

public static class CsvData
{
    public static List<T> ReadFiel<T>(string path)
    {
        using StreamReader reader = new StreamReader(path);
        using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            return csv.GetRecords<T>().ToList();
        }
    }
    
    public static void WriteFiel<T>(string path, T data)
    {
        using StreamWriter writer = new StreamWriter(path);
        using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecord(data);
        }
    }
}