using BloggingPlatform.Models;
using BloggingPlatform.Contracts;

// Ignore Spelling: Blogging
// Ignore Spelling: Blog


namespace BloggingPlatform.Interfaces
{
    public interface ICommentService
    {
        Task<FullPost?> AddToDBAsync(Guid commentId ,CommentDto comment);
        Task<FullPost?> UpdateAsync(Guid id, CommentDto updatePost);
        Task<FullPost?> GetAsync(Guid id);
        Task<List<Comment>?> GetFullAsync();
        Task <bool> DeleteAsync(Guid Id);
    }
}
