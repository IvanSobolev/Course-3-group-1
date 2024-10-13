using ConsoleApp1;
using Practices;

class Program
{
    private static readonly string FILEPATH = "sales.csv";
    
    static void Main(string[] args)
    {
        var records = SaleRecord.ReadData(FILEPATH);

        SaleRecordManager manager;
        if (records.Any())
            manager = new SaleRecordManager(records.ToList());
        else
            manager = new SaleRecordManager();

        // Добавление записей
        manager.AddRecord(new SaleRecord { Date = DateTime.Now, Product = "Product A", Region = "Russia", Quantity = 10, Price = 100.00m });
        manager.AddRecord(new SaleRecord { Date = DateTime.Now, Product = "Product B", Region = "Russia", Quantity = 5, Price = 50.00m });

        // Чтение всех записей
        
        Console.WriteLine("All Records:");
        foreach (var record in records)
        {
            Console.WriteLine($"{record.Date}, {record.Product}, {record.Quantity}, {record.Price:C}");
        }

        // Фильтрация данных по дате
        var filteredByDate = manager.FilterRecordsByDate(new DateTime(2023, 6, 1), new DateTime(2023, 12, 31));
        Console.WriteLine("Records filtered by date:");
        foreach (var record in filteredByDate)
        {
            Console.WriteLine($"{record.Date}, {record.Product}, {record.Quantity}, {record.Price:C}, {record.Region}");
        }

        // Общая сумма продаж
        var totalSales = manager.GetTotalSales();
        Console.WriteLine($"\nTotal Sales: {totalSales:C}");

        // Среднее количество проданных товаров
        var averageQuantity = manager.GetAverageQuantitySold();
        Console.WriteLine($"\nAverage Quantity Sold: {averageQuantity}");

        // Общая сумма продаж по продукту
        var totalSalesByProduct = manager.GetTotalSalesByProduct("Laptop");
        Console.WriteLine($"\nTotal Sales for Laptop: {totalSalesByProduct:C}");

        // Фильтрация данных по региону
        var filteredByRegion = manager.FilterRecordsByRegion("Europe");
        Console.WriteLine("\nRecords filtered by region:");
        foreach (var record in filteredByRegion)
        {
            Console.WriteLine($"{record.Date}, {record.Product}, {record.Quantity}, {record.Price:C}, {record.Region}");
        }
    }
}