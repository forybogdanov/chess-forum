using GameWebsite.Service.Models.Categories;
using GameWebsite.Service.Models.Comments;
using GameWebsite.Service.Models.Posts;
using GameWebsite.Service.Models.Users;
using GameWebsite.Services.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameWebsite.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService service;

        public CommentController(ICommentService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpGet("Comment/Create/{postId}")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [Route("Comment/Create/{postId}")]
        [HttpPost("Comment/Create/{postId}")]
        public async Task<IActionResult> Create(long postId, CommentDto commentDto)
        {
            commentDto.CreatedBy = new GameWebsiteUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            commentDto.Post = new PostDto
            {
                Id = postId,
                Category = new CategoryDto
                {
                    CreatedBy = new GameWebsiteUserDto { }
                },
                CreatedBy = new GameWebsiteUserDto { },
                Comments = new List<CommentDto>()
            };
            await this.service.CreateComment(commentDto);
            return Redirect("/Home");
        }
    }
}
