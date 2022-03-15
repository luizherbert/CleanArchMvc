using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetById(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();


            var category = await _categoryService.GetById(id.Value);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetById(id);
            return View(category);
        }


    }
}
