namespace PametnoMesto.Scripts;

public class DataHandler
{
    private readonly List<Vehicle> _vehicles = new();
    private int _nextId = 1;

    public int AddVehicle(string name, string color, VehicleType type)
    {
        var vehicle = new Vehicle(_nextId++, name, color, type);
        _vehicles.Add(vehicle);
        return vehicle.Id;
    }

    public bool RemoveVehicle(int id)
    {
        var v = _vehicles.FirstOrDefault(x => x.Id == id);
        if (v == null) return false;
        _vehicles.Remove(v);
        return true;
    }

    public Vehicle? GetVehicle(int id) => _vehicles.FirstOrDefault(x => x.Id == id);
    public List<Vehicle> GetVehicles() => _vehicles;
}
