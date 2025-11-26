

using Book.Enums;

namespace Book.Models.DTO.Stories
{
    public record UpdateStorie(
        string? title = null,
        string? description = null,
        Tags? tags = null,
        int? firstPage = null,
        bool? statut = null
    );
    
}
