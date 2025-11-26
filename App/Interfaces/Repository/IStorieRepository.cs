using Book.Models;
using Book.Models.DTO.Stories;


namespace App.Interfaces.Repository
{
    public interface IStorieRepository
    {
        Task<Storie> GetStorie(int id);
        Task<List<Storie>> GetAllStories();
        Task<Storie> CreateStorie(CreateStorie newStorie);
        Task<Storie> UpdateStorie(int id, UpdateStorie newData);
        Task DeleteStorie(int id);
    }
}
