using System.Text.Json.Serialization;

namespace BlogPTC.Application.Dtos
{
    public class PostDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public UserDTO User { get; set; }
    }
}
