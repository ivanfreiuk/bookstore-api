using BookStore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class CategoryService: BaseService, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var categoryEntity = await _uow.Categories.GetAsync(id);

            var categoryDto = _mapper.Map<Category, CategoryDto>(categoryEntity);

            return categoryDto;
        }

        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync()
        {
            var categoryEntities = await _uow.Categories.GetAllAsync();

            var categoryDtos = _mapper.Map<ICollection<Category>, ICollection<CategoryDto>>(categoryEntities);

            return categoryDtos;
        }
        
        public async Task AddCategoryAsync(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<CategoryDto, Category>(category);

            await _uow.Categories.AddAsync(categoryEntity);

            await _uow.SaveAsync();
            category.Id = categoryEntity.Id;
        }

        public async Task RemoveCategoryAsync(int id)
        {
            var categoryEntity = await _uow.Categories.GetAsync(id);

            await _uow.Categories.RemoveAsync(categoryEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateCategoryAsync(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<CategoryDto, Category>(category);

            await _uow.Categories.UpdateAsync(categoryEntity);

            await _uow.SaveAsync();
        }

        
    }
}
