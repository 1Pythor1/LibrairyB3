using App.Models.DTO.Choices;
using Book.Models;

namespace App.Interfaces.Repository
{
    public interface IChoiceRepository
    {
        Task<Choice[]> GetAllChoicesFromPage(int id);
        Task<Choice> CreateChoice(CreateChoice newChoice);
        Task<Choice> UpdateChoice(int id, UpdateChoice newData);
        Task DeleteChoice(int id);
    }
}
