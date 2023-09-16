using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Service
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        bool UpdateCategory(int categoryId, Category category);
        bool DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        List<Category> GetAllCategoriesByUserId(string userId);
        object GetCategoriesByUserId(int userId);
    }
}
