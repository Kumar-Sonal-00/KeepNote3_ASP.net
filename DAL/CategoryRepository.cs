using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KeepDbContext _dbContext;

        public CategoryRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*
        * This method should be used to save a new category.
        */
        public Category CreateCategory(Category category)
        {
            // Placeholder: Implement code to save the category in the database
            // Example:
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        /* This method should be used to delete an existing category. */
        public bool DeleteCategory(int categoryId)
        {
            // Placeholder: Implement code to delete the category from the database
            // Example:
            var categoryToDelete = _dbContext.Categories.Find(categoryId);
            if (categoryToDelete != null)
            {
                _dbContext.Categories.Remove(categoryToDelete);
                _dbContext.SaveChanges();
                return true; // Return true if deletion is successful
            }
            return false; // Return false if category is not found
        }

        //* This method should be used to get all categories by userId.
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            // Placeholder: Implement code to retrieve all categories for a given userId
            // Example:
            var categories = _dbContext.Categories.Where(c => c.CategoryCreatedBy == userId).ToList();
            return categories;
        }

        /*
         * This method should be used to get a category by categoryId.
         */
        public Category GetCategoryById(int categoryId)
        {
            // Placeholder: Implement code to retrieve a category by categoryId
            // Example:
            var category = _dbContext.Categories.Find(categoryId);
            return category;
        }

        /*
        * This method should be used to update an existing category.
        */
        public bool UpdateCategory(Category category)
        {
            // Placeholder: Implement code to update the category in the database
            // Example:
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return true; // Return true if the update is successful
        }

        bool ICategoryRepository.UpdateCategory(int categoryId, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
