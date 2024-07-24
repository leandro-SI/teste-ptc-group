using System.Text.Json.Serialization;

namespace BlogPTC.Application.Dtos
{
    public class UpdatePostDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
