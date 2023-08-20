using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewBooks.Data;
using ViewBooks.Models;

namespace ViewBooks.Controllers
{
	public class CategorysController : Controller
	{
		ApplicationDbContext _context;
		public CategorysController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetIndexView()
		{
			return View("Index", _context.Categorys.ToList());
		}
		[HttpGet]
		public IActionResult GetDetailsView(int id)
		{
			Category category = _context.Categorys.Include(d => d.Books).FirstOrDefault(x => x.Id == id);
			return View("Details", category);
		}
		[HttpGet]
		public IActionResult GetCreateView()
		{

			return View("Create");
		}
		[HttpGet]
		public IActionResult GetEditView(int id)
		{
			Category category = _context.Categorys.FirstOrDefault(x => x.Id == id);

			if (category == null)
			{
				return NotFound();
			}
			else
			{
				return View("Edit", category);
			}
		}

		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			Category category = _context.Categorys.Include(d => d.Books).FirstOrDefault(x => x.Id == id);

			if (category == null)
			{
				return NotFound();
			}
			else
			{
				return View("Delete", category);
			}
		}

		[HttpPost]
		public IActionResult AddNew(Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Categorys.Add(category);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Create");
		}

		[HttpPost]
		public IActionResult EditCurrent(Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Categorys.Update(category);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Edit");
		}
		[HttpPost]
		public IActionResult DeleteCurrent(int id)
		{
			//Department department = _context.Departments.FirstOrDefault(e => e.Id == id);
			Category category = _context.Categorys.FirstOrDefault(e => e.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			else
			{
				_context.Categorys.Remove(category);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
		}
	}
}
