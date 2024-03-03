// Ignore Spelling: Blogging


namespace BloggingPlatform.Models
{
    public class Comment
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Text { get; set; }
        public Guid PostId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
