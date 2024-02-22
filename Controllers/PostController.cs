using BloggingPlatform.DataBase;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Ignore Spelling: Blogging

namespace BloggingPlatform
{
    [ApiController]
    [Route("api/controller")]
    public class PostController(BlogPlatformDbContext context) : ControllerBase
    {
        private readonly BlogPlatformDbContext _context = context;

        [HttpGet]
        public async Task< IActionResult>  GetPost ()
        {
            var posts = await _context.posts.ToListAsync();
            return Ok ( posts);
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

            _context.posts.Add(post);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            Post? post = _context.posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            _context.posts.Remove(post);

            _context.SaveChanges();
            return NoContent();
        }
    }
}
