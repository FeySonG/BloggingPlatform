﻿// Ignore Spelling: Blogging
namespace BloggingPlatform.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        public string Content { get; set; } = string.Empty; 
        public string Author { get; set; } = string.Empty;

        public DateTime Date {  get; set; }

    }
}
