namespace LibraryApp.Entities.DTOs
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}