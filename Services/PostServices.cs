using BloggingPlatform.Contracts;
using BloggingPlatform.DataBase;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// Ignore Spelling: Blogging, Blog



namespace BloggingPlatform.Services
{
    public class PostServices(BlogPlatformDbContext context) : IPostService
    {
        private readonly BlogPlatformDbContext _context = context;

        public async Task<FullPost> AddToDBAsync(PostDto post)
        {
            var newPost = new FullPost
            {
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
            };

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return  newPost;
        }

        public async Task<FullPost?> DeleteAsync(Guid Id)
        {
            var post = await _context.Posts.FindAsync(Id);
            if (post == null)
            return null;
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<FullPost>?> GetFullAsync()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        public async Task<List<FullPost>?> GetAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<FullPost?> UpdateAsync(Guid id, PostDto post)
        {
            var updatePost = _context.Posts.FirstOrDefault(upPost => upPost.Id == id);
            if(updatePost == null) return null;

            updatePost.Title = (!string.IsNullOrWhiteSpace(post.Title) && post.Title != "string") ? post.Title : updatePost.Title;
            updatePost.Content = (!string.IsNullOrWhiteSpace(post.Content) && post.Content != "string") ? post.Content : updatePost.Content;
            updatePost.Author = (!string.IsNullOrWhiteSpace(post.Author) && post.Author != "string") ? post.Author : updatePost.Author;

            await _context.SaveChangesAsync();
            return updatePost;
        }

    }
}