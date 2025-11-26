using App.Error;
using App.Interfaces.Repository;
using Book.Error;
using Book.Models;
using Book.Models.DTO.Stories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class StorieRepository : IStorieRepository
    {
        public delegate Task<Page> GetPage(int id);

        private readonly LibrairyContext _dbContext;
        private readonly GetPage _getPage;

        public StorieRepository(LibrairyContext dbContext, GetPage getPage)
        {
            _dbContext = dbContext;
            _getPage = getPage;
        }

        public async Task<Storie> GetStorie(int id) =>
            await _dbContext.Storie.FindAsync(id)
            ?? throw new NotFound();
        public async Task<List<Storie>> GetAllStories() =>
            await _dbContext.Storie.ToListAsync()
            ?? throw new NotFound();
        public async Task<Storie> CreateStorie(CreateStorie newStorie)
        {
            Storie storie = newStorie.toStorie();

            _dbContext.Storie.Add(storie);
            await _dbContext.SaveChangesAsync();

            return storie;
        }
        public async Task<Storie> UpdateStorie(int id, UpdateStorie newData)
        {
            Storie storie = await _dbContext.Storie.FindAsync(id)
                ?? throw new NotFound();

            storie.Title = newData.title ?? storie.Title;
            storie.Statut = newData.statut ?? storie.Statut;            
            storie.Description = newData.description ?? storie.Description;

            if(newData.firstPage is not null)
            {
                Page firstPage = await _getPage(newData.firstPage.Value)
                    ?? throw new NotFound();

                if(firstPage.StorieId != storie.Id)
                    throw new UnAuthorize();

                storie.FirstPage = newData.firstPage;
            }

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
