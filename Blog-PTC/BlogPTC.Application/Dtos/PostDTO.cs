using BlogPTC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogPTC.Application.Dtos
{
    public class PostDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public UserDTO User { get; set; }
    }
}
