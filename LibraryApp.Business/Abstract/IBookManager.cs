using LibraryApp.Core.Utilities.Result;
using LibraryApp.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Business.Abstract
{
    public interface IBookManager
    {
        IResult Add(CreateOrUpdateBookDTO book, string firstName, string lastName);
        //Task<IResult> InsertBookAsync(CreateOrUpdateBookDTO book, string firstName, string lastName);
        IResult Remove(Guid id, string firstName, string lastName);
        IResult Update(CreateOrUpdateBookDTO book, string firstName, string lastName);
    }
}