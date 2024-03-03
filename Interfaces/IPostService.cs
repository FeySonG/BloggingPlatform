using BloggingPlatform.Contracts;
using BloggingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// Ignore Spelling: Blogging, Blog



namespace BloggingPlatform.Interfaces
{
    public interface IPostService
    {
        Task<FullPost> AddToDBAsync(PostDto post);

        Task<FullPost?> UpdateAsync(Guid id, PostDto updatePost);
        Task<List<FullPost>?> GetAsync();
        Task<List<FullPost>?> GetFullAsync();
        Task<FullPost?> DeleteAsync(Guid Id);
    }
}
