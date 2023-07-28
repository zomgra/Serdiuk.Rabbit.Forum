using Serdiuk.Rabbit.Core.Models;

namespace Serdiuk.Rabbit.Core.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Creator { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public ForumViewModel Forum { get; set; }
    }
}