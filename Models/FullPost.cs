using Newtonsoft.Json;

// Ignore Spelling: Blogging

namespace BloggingPlatform.Models
{
    public class FullPost
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public List<Comments> Comments { get; set; } = [];
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
