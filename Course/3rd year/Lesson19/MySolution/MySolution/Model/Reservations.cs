namespace MySolution.Model;

public struct Reservations
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float Price { get; set; }
    public float Total { get; set; }
}