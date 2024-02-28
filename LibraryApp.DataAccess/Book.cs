using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DataAccess
{
    public class Book : BaseEntity<Guid>
    {
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Author { get; set; }
        [StringLength(512)]
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}