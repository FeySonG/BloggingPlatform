using BloggingPlatform.DataBase;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// Ignore Spelling: Blogging

namespace BloggingPlatform
{
    [ApiController]
    [Route("post/controllers")]
    public class PostController(BlogPlatformDbContext context) : ControllerBase
    {
        private readonly BlogPlatformDbContext _context = context; //Обьявил контекст как обычно но Назар решил что подсказка сделает лучше)) решил оставить так

        [HttpGet("PostsWhithoutComments",Name = "OnlyPosts")] //вывод постов без комментариев для удобства просмотра содержимого только постов
        public async Task<IActionResult> GetPost()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet("PostsWhithComments",Name = "FullPosts")]
        public async Task<IActionResult> GetFullPost()
        {
            var postsWithComments = await _context.Posts.Include(p => p.Comments).ToListAsync();

            return Ok(postsWithComments);
        }

        [HttpPost("AddNewPost", Name = "AddNewPost")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (post == null || !ModelState.IsValid ||
                string.IsNullOrWhiteSpace(post.Title) ||
                string.IsNullOrWhiteSpace(post.Content) ||
                string.IsNullOrWhiteSpace(post.Author)) //проверка правильного заполнения модели моей сущности и на null
            {
                return BadRequest("Неподерживаемый формат или неверные данные!");
            }

                var  newPost = new FullPost
                {
                      Title = post.Title,
                      Author = post.Author,
                      Content = post.Content,
                };

            await  _context.Posts.AddAsync(newPost);
            await  _context.SaveChangesAsync(); 

            return Ok(newPost);
        }


        [HttpDelete("DeletePost", Name = "DeletePost")]
        public async Task< IActionResult> DeletePost(Guid id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("UpdatePost", Name = "UpdatePost")]
        public async Task <IActionResult> UpdatePost(Guid postId, [FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Неподерживаемый формат!");
            }

            var updatePost = _context.Posts.FirstOrDefault(upPost => upPost.Id == postId);
            if (updatePost == null)
            {
                return NotFound("Пост не найден");
            }


            //проверка трех полей на на дефолтное значение  и null

            updatePost.Title = (!string.IsNullOrWhiteSpace(post.Title) && post.Title != "string") ? post.Title : updatePost.Title;
            updatePost.Content = (!string.IsNullOrWhiteSpace(post.Content) && post.Content != "string") ? post.Content : updatePost.Content;
            updatePost.Author = (!string.IsNullOrWhiteSpace(post.Author) && post.Author != "string") ? post.Author : updatePost.Author; 

           await  _context.SaveChangesAsync();

            return Ok(updatePost);
        }


    }
}
