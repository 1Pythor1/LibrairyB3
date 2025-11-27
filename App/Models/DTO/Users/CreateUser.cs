namespace App.Models.DTO.Users
{
    public record CreateUser(        
        bool isAdmin,
        bool isWritter
    )
    {
        public User toUser() =>
            new User(isAdmin, isWritter);
    };
}
