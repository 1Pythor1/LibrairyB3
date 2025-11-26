using Book.Enums;

namespace Book.Models.DTO.Stories
{
    public record CreateStorie(        
        string title,
        string description,
        Tags? tags = null
    )
    { 
        public Storie toStorie() =>
            new Storie(title, description, tags);
    };
}
