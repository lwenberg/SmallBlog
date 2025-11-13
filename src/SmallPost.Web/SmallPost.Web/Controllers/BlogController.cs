using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SmallPost.Web.ViewModels;
using SmallPost.Domain.DTOs.BlogDTOs;
using SmallPost.Domain.Services.BlogService;

namespace SmallPost.Web.Controllers
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
        [AllowAnonymous]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            int pageSize = 4;

            try
            {
                var blogsPagination = await _blogService.GetPaginatedBlogsAsync(pageIndex, pageSize, HttpContext.RequestAborted);
                var blogView = BlogsViewModel.Create(blogsPagination);

                return View(blogView);
            }
            catch (OperationCanceledException) when (HttpContext.RequestAborted.IsCancellationRequested)
            {
                return BadRequest();
            }
        }

        // GET: BlogController/Details/<id>
        [HttpGet]
        [AllowAnonymous]
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
        public async Task<IActionResult> Create(CreateBlogDTO blogDto)
        {

            if (!ModelState.IsValid)
            {
                return View(blogDto);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _blogService.CreateAsync(blogDto, currentUser!);

            if (!result)
            {
                return View(blogDto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: BlogController/Edit/<id>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var restul = await _blogService.GetByIdAsync(id);

            if (restul is null)
            {
                return NotFound();
            }

            return View(restul);
        }

        // POST: BlogController/Edit/<id>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogDTO blog)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }

            var result = await _blogService.UpdateAsync(blog);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { blog.Id });

        }

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
