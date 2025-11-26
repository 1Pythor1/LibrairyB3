
using Book.Models;

namespace App.Models.DTO.Pages
{
    public record CreatePage(int storieId, string text = "", bool isEnd = false)
    {
        public Page toPage() =>
            new Page(text, isEnd, storieId);
    };
    
}
