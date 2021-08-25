using ComicBookStore.Models;
using ComicBookStore.Repositories;
using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Services
{
    public class CategoryTypeService
    {
        public readonly ICategoryTypeRepository _categoryTypeRepository;

        public CategoryTypeService(ICategoryTypeRepository categoryTypeRepository)
        {
            _categoryTypeRepository = categoryTypeRepository;
        }

        // GET: Get all category types
        public async Task<ActionResult<IEnumerable<CategoryTypeDTO>>> GetCategoryTypeDTO()
        {
            return await _categoryTypeRepository.Get(CategoryTypeDTO.CategoryTypeSelector);
        }

        // GET: Get category type by id
        public async Task<ActionResult<CategoryTypeDTO>> GetByIdCategoryTypeDTO(int id)
        {
            return await _categoryTypeRepository.GetById(id, CategoryTypeDTO.CategoryTypeSelector);
        }

        // POST: Post category type
        public async Task<ActionResult<bool>> Post(CategoryType categoryType)
        {
            return await _categoryTypeRepository.Post(categoryType);
        }

        // EDIT: Edit category type
        public async Task<ActionResult<bool>> Edit(CategoryType categoryType)
        {
            return await _categoryTypeRepository.Edit(categoryType);
        }

        // DELETE: Delete category type
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _categoryTypeRepository.Delete(id);
        }
    }
}
