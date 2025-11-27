

using Book.Models;

namespace DAL.Models.Users
{
    public class UserStorieItem
    {
        public int Id { get; set; }

        public int UserStorieGroupId { get; set; }
        public UserStorieGroup UserStorieGroup { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }

    }
}
