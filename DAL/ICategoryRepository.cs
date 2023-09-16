using System.Collections.Generic;
using Entities;

namespace DAL
{
    /*
	 * Should not modify this interface. You have to implement these methods in
	 * corresponding Impl classes
	 */
    public interface ICategoryRepository
    {
        Category CreateCategory(Category category);
        bool UpdateCategory(int categoryId, Category category);
        bool DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        List<Category> GetAllCategoriesByUserId(string userId);
    }
}
