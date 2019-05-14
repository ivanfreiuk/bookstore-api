using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<ICollection<CommentDto>> GetAllCommentsAsync()
        {
            var commentEntities =  await _uow.Comments.GetAllAsync();

            var commentDtos = _mapper.Map<ICollection<Comment>, ICollection<CommentDto>>(commentEntities);

            return commentDtos;
        }

        public async Task<CommentDto> GetCommentAsync(int id)
        {
            var commentEntity = await _uow.Comments.GetAsync(id);

            var commentDto = _mapper.Map<Comment,CommentDto>(commentEntity);

            return commentDto;
        }

        public async Task<ICollection<CommentDto>> GetCommentsByBookIdAsync(int bookId)
        {
            var commentEntities = await _uow.Comments.GetCommentsByBookIdAsync(bookId);

            var commentDtos = _mapper.Map<ICollection<Comment>, ICollection<CommentDto>>(commentEntities);

            return commentDtos;
        }

        public async Task AddCommentAsync(CommentDto comment)
        {
            var commentEntity = _mapper.Map<CommentDto, Comment>(comment);

            await _uow.Comments.AddAsync(commentEntity);

            await _uow.SaveAsync();
            comment.Id = commentEntity.Id;
        }

        public async Task RemoveCommentAsync(int id)
        {
            var comment = await _uow.Comments.GetAsync(id);

            await _uow.Comments.RemoveAsync(comment);

            await _uow.SaveAsync();
        }

        public async Task UpdateCommentAsync(CommentDto comment)
        {
            var commentEntity = _mapper.Map<CommentDto, Comment>(comment);

            await _uow.Comments.UpdateAsync(commentEntity);

            await _uow.SaveAsync();
        }
    }
}