namespace PametnoMesto.Scripts;

public class DataHandler
{
    List<Vehicle> _vehicles = new();

    public int AddVehicle(Vehicle vehicle)
    {
        if(_vehicles.Contains(vehicle)) return -1;

        _vehicles.Add(vehicle);
        return _vehicles.IndexOf(vehicle);
    }

    public bool RemoveVehicle(int index)
    {
        if(_vehicles.Count <= index) return false;
        _vehicles.RemoveAt(index);
        return true;
    }

    public Vehicle? GetVehicle(int index)
    {
        if(_vehicles.Count > index) return _vehicles[index];
        return null;
    }
}