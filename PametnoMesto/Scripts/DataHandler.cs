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

    public bool RentVehicle(int vehicleId, string username)
    {
        var vehicle = GetVehicle(vehicleId);
        // POPRAVEK: Uporabljamo IsAvailable
        if (vehicle == null || !vehicle.IsAvailable) return false;

        // POPRAVEK: Nastavimo IsAvailable na false
        vehicle.IsAvailable = false;
        vehicle.RenterUsername = username;
        vehicle.RentStartTime = DateTime.Now;

        return true;
    }

    public string ReturnVehicle(int vehicleId)
    {
        var vehicle = GetVehicle(vehicleId);
        // POPRAVEK: Preverimo IsAvailable
        if (vehicle == null || vehicle.IsAvailable) return "Napaka: Vozilo ni v najemu.";

        var endTime = DateTime.Now;
        var duration = endTime - vehicle.RentStartTime;
        double totalMinutes = duration.Value.TotalMinutes;

        decimal pricePerMinute = 0;
        switch (vehicle.Type)
        {
            case VehicleType.Bike: pricePerMinute = 0.05m; break;
            case VehicleType.Scooter: pricePerMinute = 0.20m; break;
            case VehicleType.Car: pricePerMinute = 0.50m; break;
        }

        decimal cost = (decimal)totalMinutes * pricePerMinute;
        if (cost < 0.10m) cost = 0.10m; 

        var player = GetPlayerByUsername(vehicle.RenterUsername);
        if (player != null)
        {
            player.Balance -= cost;
        }

        // POPRAVEK: Sprostimo vozilo (IsAvailable = true)
        vehicle.IsAvailable = true;
        vehicle.RenterUsername = null;
        vehicle.RentStartTime = null;

        return $"Vožnja končana! Čas: {Math.Round(totalMinutes, 1)} min. Cena: {cost.ToString("C")}";
    }
    
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

    public VehicleHub? GetVehicleHub(int? id) => _vehicleHubs.FirstOrDefault(h => h.Id == id);

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
    
    // Namesto Dictionary zdaj uporabljamo List<Player>
    private List<Player> _players = new List<Player>();
    private int _nextPlayerId = 1;

    public DataHandler()
    {
        // V konstruktorju dodamo admina ročno
        // "hardcodan" admin: ID 1, ime "admin", geslo "geslo123"
        _players.Add(new Player(_nextPlayerId++, "admin", "geslo123"));
    }

    // Preveri prijavo (išče po listi igralcev)
    public bool ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return false;

        // Poišči igralca, ki ima isto ime IN isto geslo
        var player = _players.FirstOrDefault(p => p.Username == username && p.Password == password);
            
        // Če smo ga našli (ni null), je prijava uspešna
        return player != null;
    }

    // Registracija (doda novega igralca v listo)
    public bool RegisterUser(string username, string password)
    {
        // Najprej preverimo, če uporabnik s tem imenom že obstaja
        if (_players.Any(p => p.Username == username))
        {
            return false; // Ime je zasedeno
        }

        // Ustvarimo novega igralca in ga dodamo v listo
        var newPlayer = new Player(_nextPlayerId++, username, password);
        _players.Add(newPlayer);
            
        return true;
    }
    
    // Metoda, ki vrne celotnega Playerja glede na uporabniško ime
    public Player? GetPlayerByUsername(string username)
    {
        return _players.FirstOrDefault(p => p.Username == username);
    }
    #endregion
}
