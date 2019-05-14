using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto> GetAuthorAsync(int id);

        Task<ICollection<AuthorDto>> GetAllAuthorsAsync();

        Task AddAuthorAsync(AuthorDto author);

        Task RemoveAuthorAsync(int id);

        Task UpdateAuthorAsync(AuthorDto author);
    }
}