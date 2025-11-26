using App.Error;
using App.Interfaces.Repository;
using App.Models.DTO.Pages;
using Book.Error;
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class PageRepository : IPageRepository
    {
        private readonly LibrairyContext _dbContext;

        public PageRepository(LibrairyContext dbContext)
        {
            _dbContext = dbContext;
        }
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
        public async Task<Page> CreatePage(CreatePage newPage)
        {
            Page page = newPage.toPage();

            _dbContext.Page.Add(page);
            await _dbContext.SaveChangesAsync();

            return page;
        }
        public async Task<Page> UpdatePage(int id, UpdatePage newData)
        {
            Page page = await _dbContext.Page.FindAsync(id)
                ?? throw new NotFound();

            page.Text = newData.text ?? page.Text;
            page.IsEnd = newData.isEnd ?? page.IsEnd;

            await _dbContext.SaveChangesAsync();

            return page;
        }
        public async Task DeletePage(int id)
        {
            Page page = await _dbContext.Page.FindAsync(id)
                ?? throw new NotFound();

            _dbContext.Page.Remove(page);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Page> PageAddChoices(int id, Choice[] choices)
        {
            Page pageNext;

            Page page = await _dbContext.Page.FindAsync(id)
                ?? throw new NotFound();

            if(page.IsEnd)
                throw new UnAuthorize();

            foreach (Choice choice in choices) 
            {
                pageNext = await _dbContext.Page.FindAsync(choice.NextPage)
                    ?? throw new NotFound();

                if (pageNext.StorieId != page.StorieId)
                    throw new UnAuthorize();
                
                page.Choices.Add(choice);
            }            

            await _dbContext.SaveChangesAsync();

            return page;
        }
        public async Task<Page> PageRemoveChoice(int id, Choice[] choices)
        {
            Page page = await _dbContext.Page.FindAsync(id)
                ?? throw new NotFound();

            HashSet<int> idsToRemove = new HashSet<int>(choices.Select(c => c.Id));
            page.Choices.RemoveAll(c => idsToRemove.Contains(c.Id));

            await _dbContext.SaveChangesAsync();

            return page;
        }
    }
}
