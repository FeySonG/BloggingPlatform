using BloggingPlatform.DataBase;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;


// Ignore Spelling: Blogging

namespace BloggingPlatform
{
    [ApiController]
    [Route("api/controller")]
    public class PostController(BlogPlatformDbContext context) : ControllerBase
    {
        private readonly BlogPlatformDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            var posts = await _context.posts.ToListAsync();
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] Post post)
        {
            if (post == null || !ModelState.IsValid ||
                string.IsNullOrWhiteSpace(post.Title) ||
                string.IsNullOrWhiteSpace(post.Content) ||
                string.IsNullOrWhiteSpace(post.Author))
            {
                return BadRequest("Неподерживаемый формат или неверные данные!");
            }

          var  newPost = new FullPost
                {
                      Title = post.Title,
                      Author = post.Author,
                      Content = post.Content,
                };

            _context.posts.Add(newPost);
            _context.SaveChanges();

            return Ok(newPost);
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePost(Guid id)
        {
            var post = _context.posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.posts.Remove(post);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{postId}")]
        public IActionResult UpdatePost(Guid postId, [FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Неподерживаемый формат!");
            }

            var updatePost = _context.posts.FirstOrDefault(upPost => upPost.Id == postId);
            if (updatePost == null)
            {
                return NotFound("Пост не найден");
            }

            updatePost.Title = (!string.IsNullOrWhiteSpace(post.Title) && post.Title != "string") ? post.Title : updatePost.Title;
            updatePost.Content = (!string.IsNullOrWhiteSpace(post.Content) && post.Content != "string") ? post.Content : updatePost.Content;
            updatePost.Author = (!string.IsNullOrWhiteSpace(post.Author) && post.Author != "string") ? post.Author : updatePost.Author;

            _context.SaveChanges();

            return Ok(updatePost);
        }


    }
}
