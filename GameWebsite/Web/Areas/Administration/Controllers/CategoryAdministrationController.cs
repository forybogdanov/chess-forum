using GameWebsite.Service.Models.Categories;
using GameWebsite.Service.Models.Users;
using GameWebsite.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameWebsite.Web.Areas.Administration.Controllers
{
    [Route("Administration/Categories")]
    public class CategoryAdministrationController : BaseAdministrationController
    {
        private readonly ICategoryService service;
        public CategoryAdministrationController(ICategoryService service)
        {
            this.service = service;
        }
        public ActionResult Index()
        { 
            return View(this.service.GetAllCategories());
        }

        [HttpGet("/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/Create")]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            categoryDto.CreatedBy = new GameWebsiteUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            await this.service.CreateCategory(categoryDto);
            return Redirect("/Administration/Categories");
        }
        [Route("Edit/{id}")]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            /*return View((await this.service.getCategoryById(id)).ToWebModel());*/
            return View((await this.service.GetCategoryById(id)));
        }
        [Route("Edit/{id}")]
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, CategoryDto categoryDto)
        {
            await this.service.UpdateCategory(id, categoryDto);

            return Redirect("/Administration/Categories");
        }
        [Route("/Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await this.service.DeleteCategory(id);

            return Redirect("/Administration/Categories");
        }
    }
}
