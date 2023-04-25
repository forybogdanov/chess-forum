using GameWebsite.Services.Categories;
using GameWebsite.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace GameWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        public HomeController(ICategoryService categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }
        public IActionResult Index()
        {
            return View(this.categoryService.GetAllCategories().ToList());
        }
    }
}
