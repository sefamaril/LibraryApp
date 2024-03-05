using LibraryApp.Core.Utilities.Result;
using LibraryApp.DataAccess;

namespace LibraryApp.Business.Abstract
{
    public interface IBookManager
    {
        IDataResult<Book> Get(Guid id);
        IDataResult<List<Book>> GetListIsAvailable();
        IResult Add(Book book, string firstName, string lastName);
        //Task<IResult> InsertBookAsync(CreateOrUpdateBookDTO book, string firstName, string lastName);
        IResult Remove(string firstName, string lastName, Guid id);
        IResult Update(Book book, string firstName, string lastName, Guid id);
    }
}