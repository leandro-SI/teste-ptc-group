namespace BlogPTC.Domain.Entities
{
    public sealed class Post
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public long UserId { get; set; }
        public User User { get; set; }

    }
}
