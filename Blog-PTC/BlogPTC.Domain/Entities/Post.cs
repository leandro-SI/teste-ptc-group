namespace BlogPTC.Domain.Entities
{
    public sealed class Post
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
