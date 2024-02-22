using BloggingPlatform.DataBase;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class PostController : ControllerBase
    {
        private readonly BlogPlatformDB _context;

        public PostController(BlogPlatformDB context)
        {
            _context = context;
        }

      
        [HttpGet]
        public  IActionResult  GetPost ()
        {
           
        return Ok(_context.Posts);
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pos = new Post
            {
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
                Date = DateTime.Now
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
