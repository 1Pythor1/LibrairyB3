using App.Error;
using App.Interfaces.Repository;
using Book.Error;
using Book.Models;
using Book.Models.DTO.Stories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class StorieRepository : BaseRepository, IStorieRepository
    {                        
        public StorieRepository(LibrairyContext dbContext) : base(dbContext) { }

        public async Task<Storie> GetStorie(int id) =>
            await _dbContext.Storie.FindAsync(id)
            ?? throw new NotFound();
        public async Task<List<Storie>> GetAllStories() =>
            await _dbContext.Storie.ToListAsync()
            ?? throw new NotFound();
        public async Task<Storie> CreateStorie(Storie newStorie)
        {            
            _dbContext.Storie.Add(newStorie);
            await _dbContext.SaveChangesAsync();

            return newStorie;
        }
        public async Task<Storie> UpdateStorie(int id, UpdateStorie newData)
        {
            Storie storie = await GetStorie(id);

            storie.Title = newData.title ?? storie.Title;
            storie.Statut = newData.statut ?? storie.Statut;   
            storie.FirstPage = newData.firstPage ?? storie.FirstPage;
            storie.Description = newData.description ?? storie.Description;            

            await _dbContext.SaveChangesAsync();

            return storie;
        }
        public async Task DeleteStorie(int id)
        {
            Storie storie = await _dbContext.Storie.FindAsync(id)
                ?? throw new NotFound();

            _dbContext.Storie.Remove(storie);
            await _dbContext.SaveChangesAsync();
        }
    }
}
