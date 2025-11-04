using Infrastructure.Repositories.BlogRespository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.DTOs.BlogDTOs;

namespace Web.Controllers
{
    [Controller]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public BlogController(IBlogRepository blogService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _blogService = blogService;
            _userManager = userManager;
            _signInManager = signInManager;
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
            {
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await _userManager.GetUserAsync(User);

            ViewBag.CanEditPost = _signInManager.IsSignedIn(User) && currentUser!.Email == blog.Author;

            return View(blog);
        }

        [Authorize]
        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogDTO blog)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if(currentUser is null) 
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _blogService.CreateAsync(blog, currentUser!);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BlogController/Edit/<id>
        [HttpGet]
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
        public async Task<IActionResult> Edit(BlogDTO blog)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var result = await _blogService.UpdateAsync(blog);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { blog.Id });

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
            if (id == 0)
            {
                return BadRequest();
            }

            var result = await _blogService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
