using LibraryApp.Business.Abstract;
using LibraryApp.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppAPI.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet("/book{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _bookManager.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("/books")]
        public IActionResult GetAll()
        {
            var result = _bookManager.GetListIsAvailable();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("/book")]
        public IActionResult Insert(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName)
        {
            var result = _bookManager.Add(bookDTO, firstName, lastName);

            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPut("/book")]
        public IActionResult Update(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName, Guid id)
        {
            var result = _bookManager.Update(bookDTO, firstName, lastName, id);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("/book")]
        public IActionResult Remove(string firstName, string lastName, Guid id)
        {
            var result = _bookManager.Remove(firstName, lastName, id);

            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
