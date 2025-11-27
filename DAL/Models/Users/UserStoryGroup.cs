using Book.Models;


namespace DAL.Models.Users
{
    public class UserStorieGroup
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int StorieId { get; set; }
        public Storie Storie { get; set; }

        public List<UserStorieItem> Items { get; set; } = new();
    }    
}
