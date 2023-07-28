using Serdiuk.Rabbit.Core.DTO.Request.Comment;
using Serdiuk.Rabbit.Core.DTO.Request.Forums;
using Serdiuk.Rabbit.Core.DTO.Responce.Comment;
using Serdiuk.Rabbit.Core.DTO.Responce.Forums;
using Serdiuk.Rabbit.Core.ViewModel;

namespace Serdiuk.Rabbit.Services.Interfaces
{
    public interface IForumService
    {
        Task<ForumEventResponce> UpdateForumAsync(UpdateForumDto dto);
        Task<ForumViewModel> GetForumByIdAsync(GetForumByIdDto dto);
        Task<ForumViewModel> CreateForumAsync(CreateForumDto dto);
        Task<ForumEventResponce> DeleteForumAsync(DeleteForumDto dto);
        Task<CommentViewModel> AddCommentInForumAsync(CreateCommentDto dto);
        Task<CommentEventResponce> DeleteCommentInForumAsync(DeleteCommentDto dto);
        Task<CommentViewModel> GetCommentByIdAsync(GetCommentByIdDto dto);
        Task<List<ForumViewModel>> GetForumsAsync();
    }
}
