using System.Text.Json.Serialization;

namespace BlogPTC.Application.Dtos
{
    public class NewPostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
