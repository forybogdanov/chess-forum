using GameWebsite.Services.Comments;
using Microsoft.AspNetCore.Mvc;

namespace GameWebsite.Web.Areas.Administration.Controllers
{
    [Route("Administration/Comments")]
    public class CommentAdministrationController : BaseAdministrationController
    {
        private readonly ICommentService service;

        public CommentAdministrationController(ICommentService service)
        {
            this.service = service;
        }
        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            this.service.DeleteComment(id);
            return Redirect("/Administration/Categories");
        }
    }
}
