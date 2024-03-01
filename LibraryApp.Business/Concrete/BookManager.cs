using LibraryApp.Business.Abstract;
using LibraryApp.Core.Utilities.Result;
using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Abstract;
using LibraryApp.Entities.DTOs;

namespace LibraryApp.Business.Concrete
{
    public class BookManager : IBookManager
    {

        private readonly IGenericRepository<Book, Guid> _bookRepository;
        private readonly IGenericRepository<Member, Guid> _memberRepository;
        private readonly LibraryContext _libraryContext;

        public BookManager(IGenericRepository<Book, Guid> bookRepository, IGenericRepository<Member, Guid> memberRepository, LibraryContext libraryContext)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _libraryContext = libraryContext;
        }

        public IResult Add(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName)
        {
            try
            {
                var member = _libraryContext.Members.FirstOrDefault(m => m.FirstName == firstName && m.LastName == lastName);

                if (member == null)
                    return new Result(false, "Kullanıcı Bulunamadı");

                var book = new Book
                {
                    Title = bookDTO.Title,
                    Author = bookDTO.Author,
                    ImageUrl = bookDTO.ImageUrl,
                    IsAvailable = true,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = member.Id
                };
                bool isSuccess = _bookRepository.AddAsync(book, member.Id).GetAwaiter().GetResult();

                return new Result(isSuccess, "Kitap Başarıyla Eklendi.");
            }
            catch (Exception ex)
            {
                return new Result(false, $"Hata : {ex.Message}");
            }
        }

        //public async Task<IResult> InsertBookAsync(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName)
        //{
        //    try
        //    {
        //        var member = await _libraryContext.Members.FirstOrDefaultAsync(m => m.FirstName == firstName && m.LastName == lastName);

        //        if (member == null)
        //            return new Result(false, "Kullanıcı Bulunamadı");

        //        var book = new Book
        //        {
        //            Title = bookDTO.Title,
        //            Author = bookDTO.Author,
        //            ImageUrl = bookDTO.ImageUrl,
        //            IsAvailable = true,
        //            CreatedDate = DateTime.UtcNow,
        //            CreatedUser = member.Id
        //        };
        //        bool isSuccess = await _bookRepository.AddAsync(book, member.Id);
        //        return new Result(isSuccess, isSuccess ? "Kitap Başarıyla Eklendi." : "Kitap eklenirken bir hata oluştu.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result(false, $"Hata : {ex.Message}");
        //    }
        //}

        public IResult Remove(Guid id, string firstName, string lastName)
        {
            if (id == null || id == Guid.Empty)
                return new Result(false, "id alanı boş geçilemez");

            try
            {
                var member = _libraryContext.Members.FirstOrDefault(m => m.FirstName == firstName && m.LastName == lastName);

                if (member == null)
                    return new Result(false, "Kullanıcı bulunamadı.");

                var book = new Book
                {
                    DeletedDate = DateTime.UtcNow,
                    DeletedUser = member.Id
                };
                bool isSuccess = _bookRepository.SoftDeleteAsyncById(book.Id, member.Id).GetAwaiter().GetResult();
                return new Result(isSuccess, "Kitap başarıyla silindi.");

            }
            catch (Exception ex)
            {

                return new Result(false, $"Hata: {ex.Message}");
            }
        }

        public IResult Update(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName)
        {
            try
            {
                var member = _libraryContext.Members.FirstOrDefault(m => m.FirstName == firstName && m.LastName == lastName);

                if (member == null)
                    return new Result(false, "Kullanıcı bulunamadı");


                var book = new Book
                {
                    Title = bookDTO.Title,
                    Author = bookDTO.Author,
                    ImageUrl = bookDTO.ImageUrl,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = member.Id
                };
                bool isSuccess = _bookRepository.UpdateAsync(book, member.Id).GetAwaiter().GetResult();
                return new Result(isSuccess, "Kitap Başarıyla Güncellendi.");

            }
            catch (Exception ex)
            {
                return new Result(false, $"Hata: {ex.Message}");
            }
        }
    }
}