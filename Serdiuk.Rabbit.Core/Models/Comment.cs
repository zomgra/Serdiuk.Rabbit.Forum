namespace Serdiuk.Rabbit.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Creator { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Forum Forum { get; set; }
    }
}