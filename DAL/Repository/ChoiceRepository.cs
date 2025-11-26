using App.Interfaces.Repository;
using App.Models.DTO.Choices;
using Book.Error;
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ChoiceRepository : IChoiceRepository
    {
        public delegate Task PageAddChoices(int id, Choice[] choices);
        private readonly LibrairyContext _dbContext;
        
        private readonly PageAddChoices pageAddChoices;

        public ChoiceRepository(LibrairyContext dbContext, PageAddChoices pageAddChoices)
        {
            _dbContext = dbContext;
            this.pageAddChoices = pageAddChoices;
        }
        public async Task<Choice[]> GetAllChoicesFromPage(int id) =>
            await _dbContext.Choice
                .Where(p => p.PageId == id)
                .ToArrayAsync()
            ?? throw new NotFound();
        public async Task<Choice> CreateChoice(CreateChoice newChoice)
        {
            Choice choice = newChoice.toChoice();

            _dbContext.Choice.Add(choice);
            await pageAddChoices(choice.PageId, [choice]);

            return choice;
        }
        public async Task<Choice> UpdateChoice(int id, UpdateChoice newData)
        {
            Choice choice = await _dbContext.Choice.FindAsync(id)
                ?? throw new NotFound();

            choice.Description = newData.description ?? choice.Description;
            choice.NextPage = newData.nextPage ?? choice.NextPage;

            await _dbContext.SaveChangesAsync();

            return choice;
        }
        public async Task DeleteChoice(int id)
        {
            Choice choice = await _dbContext.Choice.FindAsync(id)
                ?? throw new NotFound();

            _dbContext.Choice.Remove(choice);
            await _dbContext.SaveChangesAsync();
        }
    }
}
