namespace mySolution;

public class Contact(int id, string name, string number)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string PhoneNumber { get; set; } = number;
    
    public override string ToString()
    {
        return "Id: " + Id + "\t\tName: " + Name + "\t\tPhone Number: " + PhoneNumber;
    }
}