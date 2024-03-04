using LibraryApp.Entities.DTOs;

namespace LibraryApp.DataAccess.Abstract.Profile
{
    public class ProfileBase : AutoMapper.Profile
    {
        public ProfileBase()
        {
            CreateMap<Book, CreateOrUpdateBookDTO>();
        }
    }
}
