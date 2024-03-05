using LibraryApp.Business.Abstract;
using LibraryApp.DataAccess;
using LibraryApp.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly LibraryContext _libraryContext;

        public BookController(IBookManager bookManager, LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            _bookManager = bookManager;
        }

        public IActionResult Index()
        {
            var results = _bookManager.GetListIsAvailable();
            if (!results.IsSuccess || results.Data == null)
            {
                TempData["ErrorMessage"] = "Kitaplar yüklenirken bir hata oluştu";
                return View(new List<Book>());
            }

            return View(results.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {

                var result = _bookManager.Add(book, firstName, lastName);

                if (!result.IsSuccess)
                {
                    foreach (var message in result.Messages)
                    {
                        ModelState.AddModelError(string.Empty, message);
                    }
                    return View(book);
                }

                return RedirectToAction(nameof(Index));

            }
            return View(book);
        }


    }
}
