using App.Models.DTO.Choices;
using Book.Models;

namespace App.Interfaces.Repository
{
    public interface IChoiceRepository
    {
        Task<Choice> GetChoiceById(int id);
        Task<Choice[]> GetAllChoicesFromPage(int id);
        Task<Choice> CreateChoice(Choice newChoice);
        Task<Choice> UpdateChoice(int id, UpdateChoice newData);
        Task DeleteChoice(int id);
    }
}
