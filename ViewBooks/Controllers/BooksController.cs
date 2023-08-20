using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewBooks.Data;
using ViewBooks.Models;
using Microsoft.AspNetCore.Hosting;
namespace ViewBooks.Controllers
{
    public class BooksController : Controller
    {
        
        ApplicationDbContext _context;  
        IWebHostEnvironment _webHostEnvironment;
        public BooksController(IWebHostEnvironment WebHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = WebHostEnvironment;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetIndexView()
        {
            return View("Index", _context.Books.ToList());
        }
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Book book = _context.Books.Include(e => e.category).FirstOrDefault(e => e.Id == id);
            return View("Details", book);
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
			ViewBag.TypeSelectItems = new SelectList(_context.Categorys.ToList(), "Id", "BookType");

			return View("Create");
        }
        [HttpPost]
        public IActionResult AddNew(Book book, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imageGuid = Guid.NewGuid();
                string imageName = imageGuid + imageExtension;
                string imgUrl = "\\images\\" + imageName;
                book.ImageUrl = imgUrl;
                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                //fileStream
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose(); 
            }
            else
            {
                book.ImageUrl = "\\images\\No_Image.png";
            }

            if (book.DateOfPublication>DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Date of publication must be before the current time!");
            }
            if (book.Price<=0)
            {
                ModelState.AddModelError(string.Empty, "Enter invalide price!");
            }
            if (book.NumberOfPages <= 10)
            {
                ModelState.AddModelError(string.Empty, "Enter invalide number of pages!");
            }
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
				ViewBag.TypeSelectItems = new SelectList(_context.Categorys.ToList(), "Id", "BookType");
				return View("Create");
            }
        }
        [HttpPost]
        public IActionResult EditCurrent(Book book, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                if (book.ImageUrl != "\\images\\No_Image.png")
                {
                    string oldImgPath = _webHostEnvironment.WebRootPath + book.ImageUrl;

                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imageGuid = Guid.NewGuid();
                string imageName = imageGuid + imageExtension;
                string imgUrl = "\\images\\" + imageName;
                book.ImageUrl = imgUrl;
                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                //fileStream
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose();
            }


            if (book.DateOfPublication >= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Date of publication must be before the current time!");
            }
            if (book.Price < 0)
            {
                ModelState.AddModelError(string.Empty, "Enter invalide price!");
            }
            if (book.NumberOfPages < 0)
            {
                ModelState.AddModelError(string.Empty, "Enter invalide number of pages!");
            }
            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
				ViewBag.TypeSelectItems = new SelectList(_context.Categorys.ToList(), "Id", "BookType");
				return View("Edit");
            }
        }
        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Book book = _context.Books.FirstOrDefault(e => e.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
				ViewBag.TypeSelectItems = new SelectList(_context.Categorys.ToList(), "Id", "BookType");
				return View("Edit", book);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
			Book book = _context.Books.Include(e => e.category).FirstOrDefault(e => e.Id == id);

			if (book == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", book);
            }
        }
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Book book = _context.Books.FirstOrDefault(e => e.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                if (book.ImageUrl != "\\images\\No_Image.png")
                {
                    string imgPath = _webHostEnvironment.WebRootPath + book.ImageUrl;

                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }
                _context.Books.Remove(book);
                _context.SaveChanges(true);
                return RedirectToAction("GetIndexView");
            }
        }
    }
}
 