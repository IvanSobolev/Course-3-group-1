namespace ConsoleApp1;
using CsvHelper;
using System.Globalization;

public class SaleRecord
{
    public DateTime Date { get; set; }
    public string Product { get; set; }
    
    public string Region { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public static IEnumerable<SaleRecord> ReadData(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            return csv.GetRecords<SaleRecord>().ToList();
        }
    }
    
    public static void WriteData(string filePath, IEnumerable<dynamic> data)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecords(data);
        }
    }
}