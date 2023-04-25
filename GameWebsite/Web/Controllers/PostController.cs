using GameWebsite.Data.Models;
using GameWebsite.Service.Models.Categories;
using GameWebsite.Service.Models.Comments;
using GameWebsite.Service.Models.Posts;
using GameWebsite.Service.Models.Users;
using GameWebsite.Services.Comments;
using GameWebsite.Services.Posts;
using GameWebSite.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace GameWebsite.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService service;
        private readonly ICommentService commentService;

        public PostController(IPostService service, ICommentService commentService)
        {
            this.service = service;
            this.commentService = commentService;
        }
        public IActionResult Index(long id)
        {
            List<PostDto> posts = this.service.GetAllPosts().ToList();
            posts = posts.FindAll(post => post.Category.Id == id);
            PostViewData postViewData = new PostViewData(id, posts);
            return View(postViewData);
        }

        [Authorize(Roles = "User")]
        [Route("Post/Create/{categoryId}")]
        [HttpGet("Post/Create/{categoryId}")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [Route("Post/Create/{categoryId}")]
        [HttpPost("Post/Create/{categoryId}")]
        public async Task<IActionResult> Create(long categoryId, PostDto postDto)
        {
            postDto.CreatedBy = new GameWebsiteUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            postDto.Comments = new List<CommentDto>();
            postDto.Category = new CategoryDto
            {
                Id = categoryId,
                CreatedBy = new GameWebsiteUserDto { }
            };
            await this.service.CreatePost(postDto);
            return Redirect("/Home");
        }

        [Authorize(Roles = "User")]
        [HttpGet("MyPosts/{id}")]
        public IActionResult MyPosts(string id)
        {
            List<PostDto> posts = this.service.GetAllPosts().ToList();
            List<PostDto> myPosts = posts.FindAll(post => post.CreatedBy.Id == id);
            return View(myPosts);
        }

        [Authorize(Roles = "User")]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            return View(await this.service.GetPostById(id));
        }

        [Authorize(Roles = "User")]
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, PostDto postDto)
        {
            await this.service.UpdatePost(id, postDto);

            return Redirect("/Home");
        }

        [Authorize(Roles = "User")]
        [Route("MyPosts/Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await this.service.DeletePost(id);

            return Redirect("/Home");

        }

        [Route("MyPosts/Details/{id}")]
        public async Task<IActionResult> Details(long id)
        {
            PostDto postDto = await this.service.GetPostById(id);
            List<CommentDto> comments = this.commentService.GetAllComments().FindAll(comment => comment.Post.Id == id);
            PostDetailsViewData postDetailsViewData = new PostDetailsViewData(postDto, comments);

            return View(postDetailsViewData);

        }
    }
}
