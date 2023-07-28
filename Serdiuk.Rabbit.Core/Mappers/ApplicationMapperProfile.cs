using AutoMapper;
using Serdiuk.Rabbit.Core.Models;
using Serdiuk.Rabbit.Core.ViewModel;

namespace Serdiuk.Rabbit.Core.Mappers
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<Forum, ForumViewModel>();
            CreateMap<Comment, CommentViewModel>();
        }
    }
}
