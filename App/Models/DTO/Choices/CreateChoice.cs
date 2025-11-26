

using Book.Models;

namespace App.Models.DTO.Choices
{
    public record CreateChoice(
        string description,
        int pageId,
        int nextPage
    )   
    {
        public Choice toChoice() =>
            new Choice(description, pageId, nextPage);
    }
}
