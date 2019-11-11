using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;
using BookStore.Helpers;

namespace BookStore.BusinessLogic.Services
{
    public class AuthorService: BaseService, IAuthorService
    {
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork uow, IMapperFactory factory) : base(uow)
        {
            _mapper = factory.CreateMapper();
        }

        public async Task<AuthorDto> GetAuthorAsync(int id)
        {
            var authorEntity = await _uow.Authors.GetAsync(id);

            var authorDto = _mapper.Map<Author, AuthorDto>(authorEntity);

            return authorDto;
        }

        public async Task<ICollection<AuthorDto>> GetAllAuthorsAsync()
        {
            var authorEntities = await _uow.Authors.GetAllAsync();

            var authorDtos = _mapper.Map<ICollection<Author>, ICollection<AuthorDto>>(authorEntities);

            return authorDtos;
        }

        public async Task AddAuthorAsync(AuthorDto author)
        {
            var authorEntity = _mapper.Map<AuthorDto, Author>(author);
            
            await _uow.Authors.AddAsync(authorEntity);

            await _uow.SaveAsync();
            author.Id = authorEntity.Id;
        }

        public async Task RemoveAuthorAsync(int id)
        {
            var authorEntity = await _uow.Authors.GetAsync(id);

            await _uow.Authors.RemoveAsync(authorEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateAuthorAsync(AuthorDto author)
        {
            var authorEntity = _mapper.Map<AuthorDto, Author>(author);

            await _uow.Authors.UpdateAsync(authorEntity);

            await _uow.SaveAsync();
        }
    }
}