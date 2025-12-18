using System.Threading.Channels;

namespace PametnoMesto.Scripts;

public class VehicleHub
{
    public int Id { get;}
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public List<Vehicle> Vehicles { get; } = new();
    public string Status { get; set; } = "Closed";
    
    public VehicleHub(int id, string name, string address, int capacity)
    {
        Id = id;
        Name = name;
        Address = address;
        Capacity = capacity;
    }
    
    public VehicleHub(int id, string name, string address, int capacity, string status) : this(id, name, address, capacity)
    {
        Status = status;
    }
}