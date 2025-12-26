namespace PametnoMesto.Scripts
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } 

        // Konstruktor
        public Player(int id, string username, string password, decimal initialBalance = 1000)
        {
            Id = id;
            Username = username;
            Password = password;
            Balance = initialBalance; // Vsak nov igralec dobi začetni znesek (privzeto 1000)
        }
    }
}