using App.Interfaces.Repository;
using App.Models.DTO.Choices;
using Book.Error;
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ChoiceRepository : BaseRepository, IChoiceRepository
    {       
        public delegate Task PageAddChoices(int id, Choice[] choices);        
        private readonly PageAddChoices _pageAddChoices;       

        public ChoiceRepository(LibrairyContext dbContext, PageAddChoices pageAddChoices) : base(dbContext)
        {            
            _pageAddChoices = pageAddChoices;            
        }            
        
        public async Task<Choice> GetChoiceById(int id) =>
            await _dbContext.Choice.FindAsync(id)
            ?? throw new NotFound();
        public async Task<Choice[]> GetAllChoicesFromPage(int id) =>
            await _dbContext.Choice
                .Where(p => p.PageId == id)
                .ToArrayAsync()
            ?? throw new NotFound();
        public async Task<Choice> CreateChoice(Choice newChoice) =>        
            await MakeTransaction(async () =>
            {
                _dbContext.Choice.Add(newChoice);
                await _pageAddChoices(newChoice.PageId, [newChoice]);

                return newChoice;
            });
        public async Task<Choice> UpdateChoice(int id, UpdateChoice newData)
        {
            Choice choice = await GetChoiceById(id);            

            choice.Description = newData.description ?? choice.Description;
            choice.NextPage = newData.nextPage ?? choice.NextPage;

            await _dbContext.SaveChangesAsync();

            return choice;
        }       
        public async Task DeleteChoice(int id)
        {
            Choice choice = await GetChoiceById(id);

            _dbContext.Choice.Remove(choice);
            await _dbContext.SaveChangesAsync();
        }
    }
}
