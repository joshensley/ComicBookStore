using ComicBookStore.Data;
using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public class CategoryTypeRepository : ICategoryTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<CategoryType, TResult>> selector)
        {
            var categoryTypes = await _db.CategoryTypes
                .OrderBy(x => x.Name)
                .Select(selector)
                .ToListAsync();

            return categoryTypes;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<CategoryType, TResult>> selector)
        {
            var categoryType = await _db.CategoryTypes
                .Where(i => i.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return categoryType;
        }

        public async Task<ActionResult<bool>> Post(CategoryType categoryType)
        {
            _db.CategoryTypes.Add(categoryType);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Edit(CategoryType categoryType)
        {
            _db.CategoryTypes.Update(categoryType);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Delete(int id)
        {
            var categoryType = await _db.CategoryTypes.FindAsync(id);
            _db.CategoryTypes.Remove(categoryType);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
