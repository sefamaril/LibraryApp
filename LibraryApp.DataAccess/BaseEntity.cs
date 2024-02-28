using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DataAccess
{
    public abstract class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedUser { get; set; }

        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedUser { get; set; }
    }
}
