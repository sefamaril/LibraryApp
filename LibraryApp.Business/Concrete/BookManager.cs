using AutoMapper;
using LibraryApp.Business.Abstract;
using LibraryApp.Core.Utilities.Result;
using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Abstract.Repository;
using LibraryApp.Entities.DTOs;

namespace LibraryApp.Business.Concrete
{
    public class BookManager : IBookManager
    {

        private readonly IGenericRepository<Book, Guid> _bookRepository;
        private readonly IGenericRepository<Member, Guid> _memberRepository;
        private readonly IMapper _mapper;
        private readonly LibraryContext _libraryContext;

        public BookManager
        (
            IGenericRepository<Book, Guid> bookRepository,
            IGenericRepository<Member, Guid> memberRepository,
            IMapper mapper,
            LibraryContext libraryContext
        )
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
            _libraryContext = libraryContext;
        }

        public IDataResult<Book> Get(Guid id)
        {
            var getBook = _libraryContext.Books.FirstOrDefault(book => book.Id == id);
            if (getBook == null)
                return new DataResult<Book>(false, "Kitap bulunamdı.");

            var bookt = _mapper.Map<Book>(getBook);

            return new DataResult<Book>(bookt, true);
        }

        public IDataResult<List<Book>> GetListIsAvailable()
        {
            var getBookList = _libraryContext.Books.Where(x => x.IsAvailable == true).ToList();
            if (!getBookList.Any())
                return new DataResult<List<Book>>(false, "Kitaplar bulunamdı.");

            var bookListDto = _mapper.Map<List<Book>>(getBookList);

            return new DataResult<List<Book>>(bookListDto, true);
        }

        public IResult Add(Book bookDTO, string firstName, string lastName)
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
                    IsAvailable = bookDTO.IsAvailable,
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

        public IResult Remove(string firstName, string lastName, Guid id)
        {
            if (id == null || id == Guid.Empty)
                return new Result(false, "id alanı boş geçilemez");

            try
            {
                var member = _libraryContext.Members.FirstOrDefault(m => m.FirstName == firstName && m.LastName == lastName);
                if (member == null)
                    return new Result(false, "Kullanıcı bulunamadı.");

                var getBook = _libraryContext.Books.FirstOrDefault(b => b.Id == id && b.DeletedDate == null && b.DeletedUser == null);
                if (getBook == null)
                    return new Result(false, "Kitap bulunamadı");

                getBook.Id = id;
                getBook.IsAvailable = false;
                getBook.DeletedDate = DateTime.UtcNow;
                getBook.DeletedUser = member.Id;
                bool isSuccess = _bookRepository.SoftDeleteAsyncById(getBook.Id, member.Id).GetAwaiter().GetResult();
                return new Result(isSuccess, "Kitap başarıyla silindi.");

            }
            catch (Exception ex)
            {

                return new Result(false, $"Hata: {ex.Message}");
            }
        }

        public IResult Update(Book bookDTO, string firstName, string lastName, Guid id)
        {
            try
            {
                var member = _libraryContext.Members.FirstOrDefault(m => m.FirstName == firstName && m.LastName == lastName);

                if (member == null)
                    return new Result(false, "Kullanıcı bulunamadı");

                var getBook = _libraryContext.Books.FirstOrDefault(b => b.Id == id);

                if (getBook == null)
                    return new Result(false, "Kitap bulunamadı");

                getBook.Id = id;
                getBook.Title = bookDTO.Title;
                getBook.Author = bookDTO.Author;
                getBook.ImageUrl = bookDTO.ImageUrl;
                getBook.IsAvailable = bookDTO.IsAvailable;
                getBook.ModifiedDate = DateTime.UtcNow;
                getBook.ModifiedUser = member.Id;

                bool isSuccess = _bookRepository.UpdateAsync(getBook, member.Id).GetAwaiter().GetResult();
                return new Result(isSuccess, "Kitap Başarıyla Güncellendi.");

            }
            catch (Exception ex)
            {
                return new Result(false, $"Hata: {ex.Message}");
            }
        }
    }
}