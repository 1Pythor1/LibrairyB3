
using App.Error;
using App.Interfaces.Repository;
using App.Interfaces.Service;
using Book.Error;
using Book.Models;
using Book.Models.DTO.Stories;

namespace App.Services
{
    public class StorieService : IStorieService
    {
        public delegate Task<Page> GetPage(int id);

        private readonly IStorieRepository _storieRepository;
        private readonly GetPage _getPage;

        public StorieService(IStorieRepository storieRepository, GetPage getPage)
        {
            _storieRepository = storieRepository;
            _getPage = getPage;
        }

        public async Task<Storie> GetStorie(int id) =>
            await _storieRepository.GetStorie(id);
        public async Task<List<Storie>> GetAllStories() =>
            await _storieRepository.GetAllStories();
        public async Task<Storie> CreateStorie(CreateStorie newStorie)
        {
            Storie storie = newStorie.toStorie();                        

            return await _storieRepository.CreateStorie(storie);
        }
        public async Task<Storie> UpdateStorie(int id, UpdateStorie newData)
        {
            Storie storie = await GetStorie(id);                

            if (newData.firstPage is not null)
            {
                Page firstPage = await _getPage(newData.firstPage.Value);                    

                if (firstPage.StorieId != storie.Id)
                    throw new UnAuthorize();               
            }            

            return await _storieRepository.UpdateStorie(id, newData);
        }
        public async Task DeleteStorie(int id) =>                                
            await _storieRepository.DeleteStorie(id);
        
    }
}
