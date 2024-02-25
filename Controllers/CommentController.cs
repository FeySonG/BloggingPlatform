// Ignore Spelling: Blogging

using BloggingPlatform.DataBase;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform
{
    [ApiController]
    [Route("comment/controllers")]
    public class CommentController : ControllerBase
    {
        private readonly BlogPlatformDbContext _context;
        public CommentController(BlogPlatformDbContext context) // создание контекста как мама учила))
        {
            _context = context; 
        }

        [HttpGet("AllCommentFromAllPosts")]
        public async Task<IActionResult> GetPost()
        {
            var allComments = await _context.Comments.ToListAsync();
            return Ok(allComments);
        }

        [HttpGet("PostAndComments",  Name = "Post/Comments")]
        public async Task<IActionResult> GetPostWithComments(Guid PostId)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == PostId);
            if (post == null)
            {
                return NotFound("Пост не найден!");
            }

            return Ok(new { Post = post});
        }

        [HttpGet("PostCommentsById", Name = "CommentsById")]
        public async Task<IActionResult> GetComments(Guid PostId)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == PostId);
            if (post == null)
            {
                return NotFound("Пост не найден!");
            }

            return Ok(post.Comments);
        }



        [HttpPut("UpdatePostComment/{CommentId}")]
        public async Task<IActionResult> UpdatePostComment(Guid commentId,  Comments updatedComment)
        {
            var existingComment = await _context.Comments.FindAsync(commentId);
            if (existingComment == null)
            {
                return NotFound("Комментарий не найден!");
            }

            existingComment.Text = updatedComment.Text; //заменяем только текст комментария, остальное пользователю недано

            await _context.SaveChangesAsync();

            return Ok(existingComment);
        }



        [HttpDelete("DeleteComment/{CommentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId); //поиск коментария по id
            if (comment == null)
            {
                return NotFound("Комментарий не найден!");
            }

            //поиск коментария в базе и сравнение с полученным ранее

            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Comments.Any(c => c.Id == commentId));
            if (post != null)
            {
                post.Comments.Remove(comment);// удаление с таблицы Posts
            }

            _context.Comments.Remove(comment); // удаление с общей таблицы комментариев
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddNewComment/{PostId}")]
        public async Task<IActionResult> CreateComment(Guid PostId, Comments Comment)
        {
            if (Comment == null || !ModelState.IsValid || string.IsNullOrWhiteSpace(Comment.Text)) //проверка на null и корректное заполнение модели сущности
            {
                return BadRequest("Неподерживаемый формат или неверные данные!");
            }
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == PostId);
            if (post == null)
            {
                return NotFound("Пост не найден!");
            }

            Comment.Id = Guid.NewGuid(); //генерация нового Id, чтоб не возится с дефолтным 
            Comment.PostId = PostId; // Id поста к которому относится комментарий

             await  _context.Comments.AddAsync(Comment); // сохранение комментария в общей таблице
            post.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

    }
}
