using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;
using Moq;
using Service;
using Xunit;
using System;

namespace Test.Service
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class CategoryServiceTest
    {
        #region Positive tests
        [Fact, TestPriority(3)]
        public void CreateCategoryShouldReturnCategory()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            Category category = new Category { CategoryName = "API", CategoryDescription = "Microservice", CategoryCreatedBy = "John", CategoryCreationDate=new System.DateTime() };
            mockRepo.Setup(repo => repo.CreateCategory(category)).Returns(category);
            var service = new CategoryService(mockRepo.Object);

            var actual = service.CreateCategory(category);

            Assert.IsAssignableFrom<Category>(actual);
        }

        [Fact, TestPriority(1)]
        public void GetCategoryByUserShouldReturnListOfcategory()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var userId = "John";
            mockRepo.Setup(repo => repo.GetAllCategoriesByUserId(userId)).Returns(this.GetCategories());
            var service = new CategoryService(mockRepo.Object);

            var actual = service.GetAllCategoriesByUserId(userId);

            Assert.IsAssignableFrom<List<Category>>(actual);
        }

        [Fact, TestPriority(2)]
        public void GetCategoryByIdShouldReturnCategory()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var Id = 1;
            Category category = new Category { CategoryName = "Testing", CategoryDescription = "Unit Testing", CategoryCreatedBy = "John", CategoryCreationDate = new DateTime() };

            mockRepo.Setup(repo => repo.GetCategoryById(Id)).Returns(category);
            var service = new CategoryService(mockRepo.Object);

            var actual = service.GetCategoryById(Id);

            Assert.IsAssignableFrom<Category>(actual);
            Assert.Equal("Testing", actual.CategoryName);
        }

        [Fact, TestPriority(4)]
        public void DeleteCategoryShouldReturnTrue()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var Id = 2;

            // Simulate that the repository returns a non-null category when trying to get the category by id.
            mockRepo.Setup(repo => repo.GetCategoryById(Id)).Returns(new Category());

            // Simulate that the repository returns true when trying to delete a category.
            mockRepo.Setup(repo => repo.DeleteCategory(Id)).Returns(true);

            // Create the CategoryService with the mock repository.
            var service = new CategoryService(mockRepo.Object);

            // Call the DeleteCategory method.
            var actual = service.DeleteCategory(Id);

            // Assert that the actual result is true.
            Assert.True(actual);
        }


        [Fact, TestPriority(5)]
        public void UpdateCategoryShouldReturnTrue()
        {
            int Id = 1;
            Category category = new Category { CategoryName = "Testing", CategoryDescription = "Integration Testing", CategoryCreatedBy = "John" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetCategoryById(Id)).Returns(category);
            mockRepo.Setup(repo => repo.UpdateCategory(Id, category)).Returns(true);
            var service = new CategoryService(mockRepo.Object);


            var actual = service.UpdateCategory(Id, category);
            Assert.True(actual);
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category> {
               new Category {CategoryName= "Testing", CategoryDescription="Unit Testing",CategoryCreatedBy="John" }
            };

            return categories;
        }

        #endregion Positive tests

        #region Negative tests

        [Fact, TestPriority(6)]
        public void GetCategoryByUserShouldReturnEmptyList()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var userId = "Mukesh";
            mockRepo.Setup(repo => repo.GetAllCategoriesByUserId(userId)).Returns(new List<Category>());
            var service = new CategoryService(mockRepo.Object);

            var actual = service.GetAllCategoriesByUserId(userId);

            Assert.IsAssignableFrom<List<Category>>(actual);
            Assert.Empty(actual);
        }


        [Fact, TestPriority(7)]
        public void GetCategoryByIdShouldThrowException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var categoryId = 5;

            // Setup the mockRepo to return null when GetCategoryById is called with categoryId 5.
            mockRepo.Setup(repo => repo.GetCategoryById(categoryId)).Returns((Category)null);

            var service = new CategoryService(mockRepo.Object);

            var actual = Assert.Throws<CategoryNotFoundException>(() => service.GetCategoryById(categoryId));

            Assert.Equal($"Category with id: {categoryId} does not exist", actual.Message);
        }



        [Fact, TestPriority(8)]
        public void DeleteCategoryShouldThrowException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var categoryId = 5;

            // Simulate that the repository returns false when trying to delete a category.
            mockRepo.Setup(repo => repo.DeleteCategory(categoryId)).Returns(false);

            // Create the CategoryService with the mock repository.
            var service = new CategoryService(mockRepo.Object);

            // Use Assert.Throws to catch the expected exception.
            var actualException = Assert.Throws<CategoryNotFoundException>(() => service.DeleteCategory(categoryId));

            // Assert that the exception message is as expected.
            Assert.Equal($"Category with id: {categoryId} does not exist", actualException.Message);
        }



        [Fact, TestPriority(9)]
        public void UpdateCategoryShouldThrowException()
        {
            int categoryId = 105;
            Category category = new Category { CategoryName = "Testing", CategoryDescription = "Unit Testing", CategoryCreatedBy = "John" };
            Category _category = null;
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetCategoryById(categoryId)).Returns(_category);
            mockRepo.Setup(repo => repo.UpdateCategory(categoryId, category)).Returns(false);
            var service = new CategoryService(mockRepo.Object);


            var actual = Assert.Throws<CategoryNotFoundException>(() => service.UpdateCategory(categoryId, category));
            Assert.Equal($"Category with id: {categoryId} does not exist", actual.Message);
        }

        #endregion Negative tests
    }
}
