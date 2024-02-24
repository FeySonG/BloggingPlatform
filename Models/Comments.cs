// Ignore Spelling: Blogging

using Newtonsoft.Json;

namespace BloggingPlatform.Models
{
    public class Comments
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Text { get; set; }
        public Guid? PostId { get; set; }

        [JsonProperty("date")]
        public string DateString => Date.ToString("yyyy-MM-dd");

        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
