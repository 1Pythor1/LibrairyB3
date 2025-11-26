namespace Book.Models
{
    public class Choice
    {         
        public int Id { get; set; }
        public string Description { get; set; }
        public int PageId { get; set; }
        public int NextPage { get; set; }

        public Choice() { }

        public Choice(
            string description,
            int pageId,
            int nextPage,
            int id = 0) 
        {
            Description = description;
            PageId = pageId;
            NextPage = nextPage;
            Id = id;
        }
    }

}
