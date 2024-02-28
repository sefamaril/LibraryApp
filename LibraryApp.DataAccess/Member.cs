using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DataAccess
{
    public class Member : BaseEntity<Guid>
    {
        [StringLength(256)]
        public string FirstName { get; set; }
        [StringLength(256)]
        public string LastName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(16)]
        public string PhoneNumber { get; set; }
    }
}
