using Infrastructure.Services.BlogServices;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web.Controllers
{
    [Controller]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: BlogController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }

        // GET: BlogController/Details/<id>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);

            if (blog is null)
                return NotFound(new { message = $"Blog with ID {id} not found." });

            return View(blog);
        }

        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var result = await _blogService.CreateAsync(blog);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BlogController/Edit/<id>
        public async Task<IActionResult> Edit(int id)
        {
            var restul = await _blogService.GetByIdAsync(id);

            if (restul is null)
                return NotFound();

            return View(restul);
        }

        // POST: BlogController/Edit/<id>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var result = await _blogService.UpdateAsync(blog);

            if(!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details),new {blog.Id});

        }

        //// GET: BlogController/Delete/<id>
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: BlogController/Delete/<id>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0) 
            { 
                return BadRequest();
            }

            var result = await _blogService.DeleteAsync(id);

            if(!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
