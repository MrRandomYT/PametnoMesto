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
}
