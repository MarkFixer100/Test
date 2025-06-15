using Application.CategoryDto;
using Application.ProductDto;
using AutoMapper;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    public class CategoryCase
    {
        private readonly ICategory _categoryRep;

        private readonly IMapper _mapper;
        public CategoryCase(ICategory categoryRep , IMapper mapper) {
            _categoryRep = categoryRep;
            _mapper = mapper;
        }



        public async Task<IEnumerable<CategoryDtos>> getAllCategories()
        {
            var categories = await _categoryRep.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryDtos>>(categories);
        }

        public async Task<CategoryDtos> getById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("Incorect Id");
            }

            var category = await _categoryRep.GetAsync(p => p.Id == id);

            if (category == null)
            {

                throw new Exception("Продукт не найден");
            }


            return _mapper.Map<CategoryDtos>(category);
        }





        public async Task<CategoryDtos> CreateCategoriesAsync(CreateCategoryDtos modelDto)
        {
            if (await _categoryRep.GetAsync(p => p.Name == modelDto.Name) != null)
            {
                throw new Exception(" this product exist in db");
            }

            Category category = _mapper.Map<Category>(modelDto);

            await _categoryRep.CreateAsync(category);

            return _mapper.Map<CategoryDtos>(category);
        }

        public async Task Delete(Guid id)
        {
            var category = await _categoryRep.GetAsync(p => p.Id == id);

            await _categoryRep.Remove(category);

            await _categoryRep.SaveAsync();
        }

    }
}
