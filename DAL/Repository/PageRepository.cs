using App.Interfaces.Repository;
using App.Models.DTO.Pages;
using Book.Error;
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class PageRepository : BaseRepository, IPageRepository
    {        
        public PageRepository(LibrairyContext dbContext) : base(dbContext) { }
        public async Task<Page> GetPage(int id) =>
            await _dbContext.Page
                .Include(p => p.Choices)
                .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFound();
        public async Task<Page[]> GetAllPageFromStorie(int id) =>
            await _dbContext.Page
                .Where(p => p.StorieId == id)
                .Include(p => p.Choices)
                .ToArrayAsync()
            ?? throw new NotFound();
        public async Task<Page> CreatePage(Page newPage)
        {            
            _dbContext.Page.Add(newPage);
            await _dbContext.SaveChangesAsync();

            return newPage;
        }
        public async Task<Page> UpdatePage(int id, UpdatePage newData)
        {
            Page page = await GetPage(id);

            page.Text = newData.text ?? page.Text;
            page.IsEnd = newData.isEnd ?? page.IsEnd;

            await _dbContext.SaveChangesAsync();

            return page;
        }
        public async Task DeletePage(int id)
        {
            Page page = await GetPage(id);

            _dbContext.Page.Remove(page);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Page> PageAddChoices(int id, Choice[] choices)
        {            
            Page page = await GetPage(id);
            
            page.Choices.AddRange(choices);

            await _dbContext.SaveChangesAsync();

            return page;
        }
        public async Task<Page> PageRemoveChoice(int id, Choice[] choices)
        {
            Page page = await GetPage(id);

            HashSet<int> idsToRemove = new HashSet<int>(choices.Select(c => c.Id));
            page.Choices.RemoveAll(c => idsToRemove.Contains(c.Id));

            await _dbContext.SaveChangesAsync();

            return page;
        }
    }
}
