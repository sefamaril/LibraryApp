namespace LibraryApp.Entities.DTOs
{
    public class LoanDTO
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string BorrowerName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BookDTO Book { get; set; } // İlişkili kitabın detayları
    }
}
