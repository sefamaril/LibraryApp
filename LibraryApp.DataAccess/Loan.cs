using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DataAccess
{
    public class Loan : BaseEntity<Guid>
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
        [StringLength(256)]
        public string BorrowerName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Book Book { get; set; } //Book ve Member tablomuzu Ödünç tablomuz ile ilişkilendiriyoruz | sefa
        public Member Member { get; set; }
    }
}
