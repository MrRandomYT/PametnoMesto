namespace PametnoMesto.Scripts
{
    public enum VehicleType
    {
        Car,
        Scooter,
        Bike
    }

    public class Vehicle
    {
        public int Id { get; }
        public string Name { get; }
        public string Color { get; }
        public VehicleType Type { get; }
        public bool IsAvailable { get; set; } = true; 
        public int BatteryLevel { get; set; } = 100; // only relevant for electric vehicles

        public Vehicle(int id, string name, string color, VehicleType type)
        {
            Id = id;
            Name = name;
            Color = color;
            Type = type;
        }
    }
}