// Ignore Spelling: Blogging
using BloggingPlatform.Contracts;
using BloggingPlatform.DataBase;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BloggingPlatform
{
    [ApiController]
    [Route("comment/controllers")]
    public class CommentController(BlogPlatformDbContext context, ICommentService service) : ControllerBase
    {
        private readonly ICommentService _service = service;
        private readonly BlogPlatformDbContext _context = context;

        [HttpGet("AllCommentFromAllPosts", Name = "AllComments")]
        public async Task<IActionResult> GetComments()
        {
            var allComments = await _service.GetFullAsync();
            return Ok(allComments);
        }

        [HttpGet("PostAndComments",Name = "PostAndComments")]
        public async Task<IActionResult> GetPostWithComments(Guid PostId)
        {
            var post = await _service.GetAsync(PostId);
            if (post == null)return NotFound("Пост не найден!");
            return Ok(post);
        }

        [HttpGet("CommentsById",Name = "CommentsById")]
        public async Task<IActionResult> GetComment(Guid PostId)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == PostId);
            if (post == null) return NotFound("Пост не найден!");
            return Ok(post.Comments);
        }

        [HttpPut("UpdatePostComment/{commentId}", Name = "UpdateComments")]
        public async Task<IActionResult> UpdatePostComment(Guid commentId, CommentDto updatedComment)
        {
            var post = await _service.UpdateAsync(commentId, updatedComment);
            if (post == null)  return NotFound("Пост не найден!");
            return Ok(post);
        }

        [HttpDelete("DeleteComment/{CommentId}", Name = "DeleteComments")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            if (commentId == Guid.Empty) return BadRequest("поле Id пустое либо имеет не правильный формат!"); 
            var isDeleted = await _service.DeleteAsync(commentId);
            if (isDeleted == false) return NotFound();

            return NoContent();
        }

        [HttpPost("AddNewComment/{postId}", Name = "CreateComments")]
        public async Task<IActionResult> CreateComment(Guid postId, CommentDto comment)
        {
            if (comment == null || !ModelState.IsValid || string.IsNullOrWhiteSpace(comment.Text ) || postId == Guid.Empty)
            {
                return BadRequest("Неподдерживаемый формат или неверные данные!");
            }

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return NotFound("Пост не найден!");

            var addedPost = await _service.AddToDBAsync(postId, comment);
            if (addedPost == null) return StatusCode(500, "Ошибка при добавлении комментария.");
            return Ok(addedPost);
        }

    }
}
