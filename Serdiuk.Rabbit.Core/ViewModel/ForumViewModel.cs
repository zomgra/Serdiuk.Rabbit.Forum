using Serdiuk.Rabbit.Core.Models;

namespace Serdiuk.Rabbit.Core.ViewModel
{
    public class ForumViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Creator { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
        public List<string> LikedUsers { get; set; } = new();

    }
}
