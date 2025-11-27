using Book.Models;

namespace App.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public bool IsAdmin { get; set; }
        public bool IsWritter { get; set; }

        public Dictionary<Storie, List<Page>> Stories { get; set; } = new();

        public User() { }
        public User(bool isAdmin, bool isWritter, int id = 0)
        {
            Id = id;
            IsAdmin = isAdmin;
            IsWritter = isWritter;
        }
    }
}
