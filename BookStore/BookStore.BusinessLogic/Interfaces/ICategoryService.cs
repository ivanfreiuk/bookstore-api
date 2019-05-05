using BookStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryAsync(int id);

        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();

        Task AddCategoryAsync(CategoryDto category);

        Task RemoveCategoryAsync(int id);

        Task UpdateCategoryAsync(CategoryDto category);
    }
}