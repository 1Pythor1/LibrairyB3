using Book.Enums;

namespace Book.Models
{
    public class Storie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Tags? Tags { get; set; }
        public int? FirstPage { get; set; }
        public bool Statut { get; set; }
        public int Id { get; set; } = 0;

        // constructeur vide pour EF Core
        public Storie() { }  
        public Storie(
            string title,
            string description,
            Tags? tags,
            int? firstPage = null,
            bool statut = false)
        {
            Title = title;
            Description = description;
            Tags = tags;
            FirstPage = firstPage;
            Statut = statut;            
        }
    };
}
