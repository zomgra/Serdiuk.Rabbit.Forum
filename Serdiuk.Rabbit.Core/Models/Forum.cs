namespace Serdiuk.Rabbit.Core.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public List<string> LikedUsers { get; set; } = new();
    }
}