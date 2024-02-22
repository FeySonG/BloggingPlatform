using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController
    {
        [HttpGet]
        public IActionResult GetComment(string id)
        {
            return null;
        }

        [HttpPut]
        public IActionResult PutComment(string id)
        {
            return null;
        }

        [HttpDelete]
        public IActionResult DeleteComment(string id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult CreateComment(string id)
        {
            return null;
        }

    }
}
