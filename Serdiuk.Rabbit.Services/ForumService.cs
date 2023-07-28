using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serdiuk.Rabbit.Core.DTO.Request.Comment;
using Serdiuk.Rabbit.Core.DTO.Request.Forums;
using Serdiuk.Rabbit.Core.DTO.Responce.Comment;
using Serdiuk.Rabbit.Core.DTO.Responce.Forums;
using Serdiuk.Rabbit.Core.Exceptions;
using Serdiuk.Rabbit.Core.Interfaces;
using Serdiuk.Rabbit.Core.Models;
using Serdiuk.Rabbit.Core.ViewModel;
using Serdiuk.Rabbit.Services.Interfaces;

namespace Serdiuk.Rabbit.Services
{
    public class ForumService : IForumService
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public ForumService(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentViewModel> AddCommentInForumAsync(CreateCommentDto dto)
        {
            var forum = await _context.Forums.FirstOrDefaultAsync(x => x.Id == dto.ForumId);

            if (forum == null) throw new FormatException("Forum not found, try again");
            try
            {
                var comment = new Comment
                {
                    Creator = Faker.Name.FullName(),
                    Content = dto.Data,
                };
                forum.Comments.Add(comment);
                await _context.SaveChangesAsync(CancellationToken.None);
                return _mapper.Map<CommentViewModel>(forum);
            }
            catch(Exception ex)
            {
                throw new ForumException("Error with creating forum: "+ex.Message);
            }
        }

        public async Task<ForumViewModel> CreateForumAsync(CreateForumDto dto)
        {
            try
            {
                var forum = new Forum()
                {
                    Creator = Faker.Name.FullName(),
                    Content = dto.Content,
                    Title = dto.Title,
                };
                await _context.Forums.AddAsync(forum);
                await _context.SaveChangesAsync(CancellationToken.None);
                return _mapper.Map<ForumViewModel>(forum);
            }
            catch (Exception ex)
            {
                throw new ForumException("Error with creating forum: " + ex.Message);
            }
        }

        public async Task<CommentEventResponce> DeleteCommentInForumAsync(DeleteCommentDto dto)
        {
            try
            {

                var forum = await _context.Forums.FirstOrDefaultAsync(x => x.Id == dto.ForumId);
                var commentInForum = await _context.Comments.FirstOrDefaultAsync(x => x.Id == dto.CommentId);
                if (!forum.Comments.Contains(commentInForum)) throw new ForumException("Comment not found in forum");

                forum.Comments.Remove(commentInForum);
                await _context.SaveChangesAsync(CancellationToken.None);
                return new() { Ok = true };
            }
            catch (Exception ex)
            {
                return new() { Ok = false, Errors = new() { ex.Message } };
            }
        }

        public async Task<ForumEventResponce> DeleteForumAsync(DeleteForumDto dto)
        {
            try
            {
                var forum = await _context.Forums.FirstOrDefaultAsync(x => x.Id == dto.ForumId);
                if (forum == null) throw new ForumException("Forum not found, try again");

                _context.Forums.Remove(forum);
                await _context.SaveChangesAsync(CancellationToken.None);
                return new() { Ok = true };
            }
            catch(Exception ex)
            {
                return new() { Ok = false, Errors = new() { ex.Message } };
            }
        }

        public async Task<CommentViewModel> GetCommentByIdAsync(GetCommentByIdDto dto)
        {
            var entity = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == dto.CommentId);
            if (entity == null) throw new ForumException("Comment not found");

            return _mapper.Map<CommentViewModel>(entity);
        }

        public async Task<ForumViewModel> GetForumByIdAsync(GetForumByIdDto dto)
        {
            var entity = await _context.Forums.AsNoTracking().FirstOrDefaultAsync(x => x.Id == dto.ForumId);
            if (entity == null) throw new ForumException("Forum not found");

            return _mapper.Map<ForumViewModel>(entity);
        }

        public async Task<List<ForumViewModel>> GetForumsAsync()
        {
            var entities = await _context.Forums.AsNoTracking().ToListAsync();
            return _mapper.Map<List<ForumViewModel>>(entities);
        }

        public async Task<ForumEventResponce> UpdateForumAsync(UpdateForumDto dto)
        {
            try
            {
                var entity = await _context.Forums.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) throw new ForumException("Forum not found");
                entity.Title = dto.Title;
                entity.Content = dto.Content;
                await _context.SaveChangesAsync(CancellationToken.None);
                return new() { Ok = true };
            }
            catch(Exception ex)
            {
                return new() { Ok = false, Errors = new() { ex.Message } };
            }
        }
    }
}
