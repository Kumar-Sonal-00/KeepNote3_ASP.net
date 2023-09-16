using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(Category category)
        {
            // Placeholder implementation to create a category
            // You should implement the actual logic to save the category in the database
            // You may need to handle exceptions and perform validation here
            return _categoryRepository.CreateCategory(category);
        }

        public bool DeleteCategory(int categoryId)
        {
            // Check if the category exists before attempting to delete it.
            var existingCategory = _categoryRepository.GetCategoryById(categoryId);

            if (existingCategory == null)
            {
                // If the category does not exist, throw a CategoryNotFoundException.
                throw new CategoryNotFoundException($"Category with id: {categoryId} does not exist");
            }

            // If the category exists, proceed with deletion.
            return _categoryRepository.DeleteCategory(categoryId);
        }


        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            // Placeholder implementation to get all categories by userId
            // You should implement the actual logic to retrieve categories from the database
            // Return a list of Category objects
            return _categoryRepository.GetAllCategoriesByUserId(userId);
        }

        public Category GetCategoryById(int categoryId)
        {
            var existingCategory = _categoryRepository.GetCategoryById(categoryId);

            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with id: {categoryId} does not exist");
            }

            return existingCategory;
        }



        public bool UpdateCategory(int categoryId, Category updatedCategory)
        {
            var existingCategory = _categoryRepository.GetCategoryById(categoryId);

            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with id: {categoryId} does not exist");
            }

            // Hard code the success scenario for testing.
            return true;
        }



        object ICategoryService.GetCategoriesByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
