namespace Book.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsEnd { get; set; }
        public int StorieId { get; set; }        
        public List<Choice> Choices { get; set; } = new List<Choice>();

        // Constructeur vide pour EF Core
        public Page() { }
        
        public Page(string text, bool isEnd, int storieId)
        {
            Text = text;
            IsEnd = isEnd;
            StorieId = storieId;
        }
    }
}
