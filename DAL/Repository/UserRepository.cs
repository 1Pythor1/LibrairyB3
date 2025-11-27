using App.Models;
using App.Models.DTO.Choices;
using App.Models.DTO.Users;
using Book.Error;
using Book.Models;
using DAL.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(LibrairyContext dbContext) : base(dbContext) { }

        private async Task<UserModel> GetUserModelById(int id) =>
            await _dbContext.UserModel.FindAsync(id)
            ?? throw new NotFound();
        public async Task<User> GetUserById(int id) =>
            (await _dbContext.UserModel.FindAsync(id))?.ToUser()
            ?? throw new NotFound();
        public async Task<User[]> GetAllUsers() =>
            (await _dbContext.UserModel
            .Include(u => u.StoryGroups)
            .ThenInclude(g => g.Items)
            .ToListAsync())
            .Select(u => u.ToUser())
            .ToArray();
        public async Task<User> CreateUser(CreateUser newUser)
        {
            UserModel model = UserModel.CreateFromCreateUser(newUser);
            _dbContext.UserModel.Add(model);
            await _dbContext.SaveChangesAsync();

            return model.ToUser();
        }
        public async Task<User> UpdateUser(int id, UpdateUser newData)
        {
            UserModel user = await GetUserModelById(id);

            user.IsWritter = newData.isWritter ?? user.IsWritter;
            user.IsAdmin = newData.isAdmin ?? user.IsAdmin;

            await _dbContext.SaveChangesAsync();

            return user.ToUser();
        }
        public async Task DeleteUser(int id)
        {
            UserModel user = await GetUserModelById(id);

            _dbContext.UserModel.Remove(user);
            await _dbContext.SaveChangesAsync();
        }


    }
}
