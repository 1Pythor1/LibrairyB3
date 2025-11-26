using App.Error;
using App.Interfaces.Repository;
using App.Interfaces.Service;
using App.Models.DTO.Choices;
using Book.Error;
using Book.Models;

namespace App.Services
{
    public class ChoiceService : IChoiceService
    {
        public delegate Task<Page> GetPage(int id);
        private readonly IChoiceRepository _choiceRepository;

        private readonly GetPage _getPage;

        public ChoiceService(IChoiceRepository choiceRepository, GetPage getPage)
        {
            _choiceRepository = choiceRepository;
            _getPage = getPage;
        }

        public async Task<Choice[]> GetAllChoicesFromPage(int id) =>
            await _choiceRepository.GetAllChoicesFromPage(id);
        public async Task<Choice> CreateChoice(CreateChoice newChoice)
        {
            Choice choice = newChoice.toChoice();            
            
            return await _choiceRepository.CreateChoice(choice);
        }
        public async Task<Choice> UpdateChoice(int id, UpdateChoice newData)
        {
            if(newData.nextPage is not null)
            {
                Choice choice = await _choiceRepository.GetChoiceById(id);

                Page current = await _getPage(choice.PageId);
                Page next = await _getPage(newData.nextPage.Value);

                if (current.StorieId != next.StorieId)
                    throw new UnAuthorize();
            }            

            return await _choiceRepository.UpdateChoice(id, newData);
        }
                           
        public async Task DeleteChoice(int id) =>
            await _choiceRepository.DeleteChoice(id);        
    }
}
