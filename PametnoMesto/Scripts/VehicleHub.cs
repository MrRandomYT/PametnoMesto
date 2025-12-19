namespace PametnoMesto.Scripts;

public class VehicleHub
{
    public int Id { get;}
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<Vehicle> Vehicles { get; } = new();
    public string Status { get; set; } = "Closed";
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    
    public VehicleHub(int id, string name, int capacity)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
    }
    
    public VehicleHub(int id, string name, int capacity, string status) : this(id, name, capacity)
    {
        Status = status;
    }
    public VehicleHub(int id, string name, int capacity, double latitude, double longitude, string status) : this(id, name, capacity, status)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}