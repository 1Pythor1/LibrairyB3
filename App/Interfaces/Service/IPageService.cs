
using App.Models.DTO.Pages;
using Book.Models;

namespace App.Interfaces.Service
{
    public interface IPageService
    {
        Task<Page> GetPage(int id);
        Task<Page[]> GetAllPageFromStorie(int id);
        Task<Page> CreatePage(CreatePage newPage);
        Task<Page> UpdatePage(int id, UpdatePage newData);
        Task DeletePage(int id);
        Task<Page> PageAddChoices(int id, Choice[] choices);
        Task<Page> PageRemoveChoice(int id, Choice[] choices);
    }
}
