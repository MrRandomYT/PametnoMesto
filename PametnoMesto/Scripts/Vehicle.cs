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
        public string Name { get; set; }
        public string Color { get; set; }
        public VehicleType Type { get; set; }
        public bool IsAvailable { get; set; } = false; 
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