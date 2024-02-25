// Ignore Spelling: Blogging


namespace BloggingPlatform.Models
{
    public class Comments
    {

        public Guid Id { get; set; }
        public string? Text { get; set; }
        public Guid PostId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
