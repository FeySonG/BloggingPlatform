// Ignore Spelling: Blogging

using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController
    {
        [HttpGet]
        public IActionResult? GetComment()
        {
            return null;
        }

        [HttpPut]
        public IActionResult? PutComment()
        {
            return null;
        }

        [HttpDelete]
        public IActionResult? DeleteComment()
        {
            return null;
        }

        [HttpPost]
        public IActionResult? CreateComment()
        {
            return null;    
        }

    }
}
