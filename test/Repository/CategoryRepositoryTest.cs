using System.Collections.Generic;
using DAL;
using Entities;
using Xunit;

namespace Test.Repository
{
    [Collection("Database collection")]
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class CategoryRepositoryTest
    {
        CategoryRepository repository;
        public CategoryRepositoryTest(DatabaseFixture databaseFixture)
        {
            repository = new CategoryRepository(databaseFixture.context);
        }
        [Fact,TestPriority(4)]
        public void CreateCategoryShouldSuccess()
        {
            Category category = new Category { CategoryName = "API", CategoryDescription = "Microservice", CategoryCreatedBy = "John" };

            var actual = repository.CreateCategory(category);

            Assert.IsAssignableFrom<Category>(actual);
            Assert.Equal(2, actual.CategoryId);
        }

        [Fact,TestPriority(5)]
        public void DeleteCategoryShouldSuccess()
        {
            int categoryId = 2;

            var actual = repository.DeleteCategory(categoryId);

            Assert.True(actual);
            Assert.Null(repository.GetCategoryById(categoryId));
        }

        [Fact, TestPriority(2)]
        public void GetAllCategoriesByUserIdShouldReturnList()
        {
            string userId = "John";

            var actual = repository.GetAllCategoriesByUserId(userId);
            Assert.IsAssignableFrom<IEnumerable<Category>>(actual);
            Assert.NotNull(actual);
        }

        [Fact, TestPriority(1)]
        public void GetCategoryByIdShouldReturnCategory()
        {
            int categoryId = 1;

            var actual = repository.GetCategoryById(categoryId);
            Assert.IsAssignableFrom<Category>(actual);
            Assert.Equal("Testing", actual.CategoryName);
        }

        [Fact,TestPriority(3)]
        public void UpdateCategoryShouldSuccess()
        {
            Category category = repository.GetCategoryById(1);
            category.CategoryName = "DevOps";

            var actual = repository.UpdateCategory(category);
            Assert.True(actual);
        }
    }
}
