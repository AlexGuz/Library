using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult SaveToFile(string fileType)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Books." + fileType;

                _bookService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Books." + fileType,
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}