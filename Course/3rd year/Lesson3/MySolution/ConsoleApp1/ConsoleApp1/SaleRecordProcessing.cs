namespace ConsoleApp1;

public class SaleRecordProcessing(List<SaleRecord> records)
{
    private readonly List<SaleRecord> _records = records;

    public void CreateRecord(SaleRecord newRecord)
    {
        _records.Add(newRecord ?? throw new ArgumentNullException(nameof(newRecord), "Record cannot be null"));
    }
    
    public bool UpdateRecord(int i, SaleRecord newRecord)
    {
        if (i < 0 || i >= _records.Count)
        {
            return false;
        }
        _records[i] = newRecord ?? throw new ArgumentNullException(nameof(newRecord), "Record cannot be null");
        return true;
    }

    public bool DeleteRecord(int i)
    {
        if (i < 0 || i >= _records.Count)
        {
            return false;
        }
        _records.RemoveAt(i);
        return true;
    }

    public List<SaleRecord> GetAllRecords()
    {
        return _records;
    }

    public List<SaleRecord> SortedByDate()
    {
        return new List<SaleRecord>(_records.OrderBy(r => r.Date));
    }

    public List<SaleRecord> FilterByDate(float MinPrice, float MaxPrice)
    {
        return new List<SaleRecord>(_records.Where(r => r.Price > MinPrice && r.Price < MaxPrice));
    }

    public float GetTotalSales()
    {
        return _records.Sum(r => r.Price * r.Quantity);
    }
    
    public double GetAverageQuantitySold()
    {
        return _records.Average(r => r.Quantity);
    }
    
    public float GetTotalSalesByProduct(string product)
    {
        return _records.Where(r => r.Product.Equals(product, StringComparison.OrdinalIgnoreCase))
            .Sum(r => r.Price * r.Quantity);
    }
    
    public float GetTotalSalesByDate(DateTime startDate, DateTime endDate)
    {
        return _records.Where(r => r.Date >= startDate && r.Date <= endDate)
            .Sum(r => r.Price * r.Quantity);
    }
}