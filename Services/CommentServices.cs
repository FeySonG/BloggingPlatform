
// Ignore Spelling: Blogging
// Ignore Spelling: Blog

using BloggingPlatform.Contracts;
using BloggingPlatform.DataBase;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace BloggingPlatform.Services
{
    public class CommentServices(BlogPlatformDbContext context) : ICommentService
    {
        private readonly BlogPlatformDbContext _context = context;
        public async Task<FullPost?> AddToDBAsync(Guid postId, CommentDto comment)
        {
            if (postId == Guid.Empty || comment == null)
            {
                return null;
            }

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return null;
            }

            var addComment = new Comment
            {
                Text = comment.Text,
                PostId = post.Id
            };
               post.Comments.Add(addComment);
            try
            {
                await _context.Comments.AddAsync(addComment); // сохранение комментария в общей таблице
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование или выброс исключения
                Console.WriteLine($"Error saving comment: {ex.Message}");
                return null;
            }

           
            return post;
        }

        public async Task<bool> DeleteAsync(Guid commentId)
        {

            var comment = await _context.Comments.FindAsync(commentId); //поиск коментария по id
            if (comment == null)
            {
                return false;
            }

            //поиск коментария в базе и сравнение с полученным ранее

            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Comments.Any(c => c.Id == commentId));
            post?.Comments.Remove(comment);// удаление с таблицы Posts

            _context.Comments.Remove(comment); // удаление с общей таблицы комментариев
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FullPost?> GetAsync(Guid postId)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            if(post != null) { return post;} else  return null;

           
        }

        public async Task<List<Comment>?> GetFullAsync()
        {
            var allComments = await _context.Comments.ToListAsync();

            return allComments;
        }

        public async Task<FullPost?> UpdateAsync(Guid commentId, CommentDto updatedComment)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Text = updatedComment.Text;

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Comments.Any(c => c.Id == commentId));

            if (post == null)
            {
                return null;
            }
            var endComment = post.Comments.FirstOrDefault(existingComment => existingComment.Id == commentId);
            if (endComment == null) return null;
            endComment.Text = updatedComment.Text;
            await _context.SaveChangesAsync();

            return post ;
        }
    }
}
