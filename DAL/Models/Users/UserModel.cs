using App.Models;
using App.Models.DTO.Users;
using Book.Models;


namespace DAL.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; } = 0;
        public bool IsAdmin { get; set; }
        public bool IsWritter { get; set; }

        public List<UserStorieGroup> StoryGroups { get; set; } = new();

        public UserModel() { }
        public UserModel(bool isAdmin, bool isWritter, int id = 0)
        {
            Id = id;
            IsAdmin = isAdmin;
            IsWritter = isWritter;
        }
        public static UserModel CreateFromUser(App.Models.User user)
        {
            UserModel model = new UserModel(user.IsAdmin, user.IsWritter, user.Id);

            foreach (var kvp in user.Stories)
            {
                Storie storie = kvp.Key;
                List<Page> pages = kvp.Value;

                var group = new UserStorieGroup
                {
                    UserId = user.Id,
                    StorieId = storie.Id,
                    Storie = storie,
                    Items = pages.Select(p => new UserStorieItem
                    {
                        PageId = p.Id,
                        Page = p
                    }).ToList()
                };

                model.StoryGroups.Add(group);
            }

            return model;
        }
        public static UserModel CreateFromCreateUser(CreateUser user) =>
            new UserModel(user.isAdmin, user.isWritter);
                 

        public User ToUser() 
        {
            User user = new User(IsAdmin, IsWritter, Id);
            user.Stories = StoryGroups.ToDictionary(
                sg => sg.Storie,
                sg => sg.Items.Select(i => i.Page).ToList()
            );

            return user;
        }        
    }
}
