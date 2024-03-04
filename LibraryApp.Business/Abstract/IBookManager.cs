using LibraryApp.Core.Utilities.Result;
using LibraryApp.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Business.Abstract
{
    public interface IBookManager
    {
        IDataResult<CreateOrUpdateBookDTO> Get(Guid id);
        IDataResult<List<CreateOrUpdateBookDTO>> GetListIsAvailable();
        IResult Add(CreateOrUpdateBookDTO book, string firstName, string lastName);
        //Task<IResult> InsertBookAsync(CreateOrUpdateBookDTO book, string firstName, string lastName);
        IResult Remove(string firstName, string lastName, Guid id);
        IResult Update(CreateOrUpdateBookDTO book, string firstName, string lastName, Guid id);
    }
}