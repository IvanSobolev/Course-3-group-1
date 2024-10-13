namespace ConsoleApp1;

class Program
{
    private static readonly string FILE_PATH = "sales.csv";
    
    static void Main(string[] args)
    {
        SaleRecordProcessing saleRecordProcessing = new SaleRecordProcessing(CsvData.ReadFiel<SaleRecord>(FILE_PATH));
        List<SaleRecord> sorted = saleRecordProcessing.SortedByDate();
    }
}