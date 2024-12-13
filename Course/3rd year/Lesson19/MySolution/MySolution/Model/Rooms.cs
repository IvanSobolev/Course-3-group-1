namespace MySolution.Model;

public struct Rooms
{
    public int Id { get; set; }
    public string HomeType { get; set; }
    public string Address { get; set; }
    public bool HasTv { get; set; }
    public bool HasInternet { get; set; }
    public bool HasKitchen { get; set; }
    public bool HasAirCon { get; set; }
    public float Price { get; set; }
    public int OwnerId { get; set; }
    public float Latitube { get; set; }
    public float Lonitube { get; set; }
}