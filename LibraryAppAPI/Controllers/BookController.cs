using LibraryApp.Business.Abstract;
using LibraryApp.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppAPI.Controllers
{
    [ApiController]
    [Route("/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }


        [HttpPost]
        public IActionResult Insert(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName)
        {
            var result = _bookManager.Add(bookDTO, firstName, lastName);

            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update(CreateOrUpdateBookDTO bookDTO, string firstName, string lastName)
        {
            var result = _bookManager.Update(bookDTO, firstName, lastName);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Remove(Guid Id, string firstName, string lastName)
        {
            var result = _bookManager.Remove(Id, firstName, lastName);

            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
