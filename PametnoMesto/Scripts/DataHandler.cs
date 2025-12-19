namespace PametnoMesto.Scripts;

public class DataHandler
{
    #region Vehicles

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
        var v = GetVehicle(id);
        if (v == null) return false;
        _vehicles.Remove(v);
        return true;
    }
    
    public bool UpdateVehicle(int id, string name, string color, VehicleType type, bool isAvailable)
    {
        var vehicle = GetVehicle(id);
        if (vehicle == null) return false;

        vehicle.Name = name;
        vehicle.Color = color;
        vehicle.Type = type;
        vehicle.IsAvailable = isAvailable;

        return true;
    }

    public Vehicle? GetVehicle(int id) => _vehicles.FirstOrDefault(x => x.Id == id);
    public List<Vehicle> GetVehicles() => _vehicles;

    #endregion
    
    #region VehicleHub
    
    private List<VehicleHub> _vehicleHubs = new();
    private int _nextHubId = 1;

    public List<VehicleHub> GetVehicleHubs() => _vehicleHubs;

    public int AddVehicleHub(string name, int capacity, double longitude, double latitude, string status = "Closed")
    {
        var hub = new VehicleHub(_nextHubId++, name, capacity, latitude, longitude, status);
        _vehicleHubs.Add(hub);
        return hub.Id;
    }

    public bool RemoveVehicleHub(int id)
    {
        var hub = _vehicleHubs.FirstOrDefault(h => h.Id == id);
        if (hub == null) return false;
        _vehicleHubs.Remove(hub);
        return true;
    }

    public VehicleHub? GetVehicleHub(int id) => _vehicleHubs.FirstOrDefault(h => h.Id == id);

    public bool UpdateVehicleHub(int id, string name, int capacity, double longitude, double latitude, string status)
    {
        var hub = _vehicleHubs.FirstOrDefault(h => h.Id == id);
        if (hub == null) return false;

        hub.Name = name;
        hub.Capacity = capacity;
        hub.Longitude = longitude;
        hub.Latitude = latitude;
        hub.Status = status;
        return true;
    }

    public bool IsVehicleAssigned(int vehicleId)
    {
        foreach (var hub in _vehicleHubs)
        {
            if (hub.Vehicles.Any(v => v.Id == vehicleId))
            {
                return true;
            }
        }

        return false;
    }

    
    #endregion
    
    #region Users
    
    // Slovar za shranjevanje uporabnikov (Uporabniško ime -> Geslo)
    // Dodamo privzetega admina, da lahko takoj testiraš
    private Dictionary<string, string> _users = new Dictionary<string, string>()
    {
        { "admin", "geslo123" }
    };

    // Preveri, ali so podatki za prijavo pravilni
    public bool ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
            return false;

        return _users.ContainsKey(username) && _users[username] == password;
    }

    // Registrira novega uporabnika
    public bool RegisterUser(string username, string password)
    {
        if (_users.ContainsKey(username))
        {
            return false; // Uporabnik že obstaja
        }
            
        _users.Add(username, password);
        return true; // Uspešna registracija
    }
    
    #endregion
}
