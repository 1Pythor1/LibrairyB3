
using App.Error;
using App.Interfaces.Repository;
using App.Interfaces.Service;
using App.Models.DTO.Pages;
using Book.Error;
using Book.Models;


namespace App.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository) =>
            _pageRepository = pageRepository;

        public async Task<Page> GetPage(int id) =>
            await _pageRepository.GetPage(id);
        public async Task<Page[]> GetAllPageFromStorie(int id) =>
            await _pageRepository.GetAllPageFromStorie(id);
        public async Task<Page> CreatePage(CreatePage newPage)
        {
            Page page = newPage.toPage();

            return await _pageRepository.CreatePage(page);
        }                    
        public async Task<Page> UpdatePage(int id, UpdatePage newData)
        {
            Page page = await GetPage(id);

            if (newData.isEnd == true && page.Choices.Count > 0)
                throw new UnAuthorize();            
           
            return await _pageRepository.UpdatePage(id, newData);
        }
        public async Task DeletePage(int id) =>
            await _pageRepository.DeletePage(id);
        public async Task<Page> PageAddChoices(int id, Choice[] choices)
        {
            Page pageNext;

            Page page = await GetPage(id);

            if (page.IsEnd)
                throw new UnAuthorize();

            foreach (Choice choice in choices)
            {
                pageNext = await GetPage(choice.NextPage);                    

                if (pageNext.StorieId != page.StorieId)
                    throw new UnAuthorize();                
            }            

            return await _pageRepository.PageAddChoices(id, choices);
        }
        public async Task<Page> PageRemoveChoice(int id, Choice[] choices) =>
            await _pageRepository.PageRemoveChoice(id, choices);

    }
}
