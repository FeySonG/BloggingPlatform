using BloggingPlatform.Contracts;
using BloggingPlatform.DataBase;
using BloggingPlatform.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// Ignore Spelling: Blogging

namespace BloggingPlatform
{
    [ApiController]
    [Route("post/controllers")]
    public class PostController(/*BlogPlatformDbContext context,*/ IPostService services) : ControllerBase 
    {
        //Обьявил контекст как обычно но Назар решил что подсказка сделает лучше)) решил оставить так
       // private readonly BlogPlatformDbContext _context = context;
        private readonly IPostService _services = services;
        
        //вывод постов без комментариев для удобства просмотра содержимого только постов
        [HttpGet("PostsWhithoutComments",Name = "OnlyPosts")] 
        public async Task<IActionResult> GetPost()
        {
            var posts = await _services.GetAsync();
            return Ok(posts);
        }

        [HttpGet("PostsWhithComments",Name = "FullPosts")]
        public async Task<IActionResult> GetFullPost()
        {
            var postsWithComments = await _services.GetFullAsync();
            return Ok(postsWithComments);
        }

        [HttpPost("AddNewPost", Name = "AddNewPost")]
        public async Task<IActionResult> CreatePost([FromBody] PostDto post)
        {
            //проверка правильного заполнения модели моей сущности и на null
            if (post == null || !ModelState.IsValid ||
                string.IsNullOrWhiteSpace(post.Title) ||
                string.IsNullOrWhiteSpace(post.Content) ||
                string.IsNullOrWhiteSpace(post.Author)) return BadRequest("Неподерживаемый формат или неверные данные!");

            var newPost = await  _services.AddToDBAsync(post);
            return Ok(newPost);
        }

        [HttpDelete("DeletePost", Name = "DeletePost")]
        public async Task< IActionResult> DeletePost(Guid id)
        {
           await  _services.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("UpdatePost", Name = "UpdatePost")]
        public async Task <IActionResult> UpdatePost(Guid postId, [FromBody] PostDto post)
        {
            if (post == null) return BadRequest("Неподерживаемый формат!");
            var updatePost = await _services.UpdateAsync(postId, post);
            if(updatePost == null) return NoContent();
            return Ok(updatePost);
        }


    }
}
