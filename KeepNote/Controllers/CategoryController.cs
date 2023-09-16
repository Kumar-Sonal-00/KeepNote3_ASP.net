using Microsoft.AspNetCore.Mvc;
//using KeepNote.Service;
//using KeepNote.Entities;
//using KeepNote.Exceptions;
using Entities;
using Exceptions;
using Service;
using System.Collections.Generic;
using System.Linq;

namespace KeepNote.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            try
            {
                _categoryService.CreateCategory(category);
                return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, category);
            }
            catch (CategoryAlreadyExistsException)
            {
                return Conflict("Category with the same ID already exists.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok("Category deleted successfully.");
            }
            catch (CategoryNotFoundException)
            {
                return NotFound($"Category with id: {id} does not exist");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                _categoryService.UpdateCategory(id, category);
                return Ok("Category updated successfully.");
            }
            catch (CategoryNotFoundException)
            {
                return NotFound($"Category with id: {id} does not exist");
            }
        }


        [HttpGet("{userId}")]
        public IActionResult GetCategoriesByUserId(int userId)
        {
            var categories = _categoryService.GetCategoriesByUserId(userId);

            if (categories == null)
            {
                return NotFound($"Categories for user with id: {userId} not found.");
            }

            // Check if categories is a collection or list before using Count.
            if (categories is IEnumerable<Category> categoryList)
            {
                if (categoryList.Count() == 0)
                {
                    return NotFound($"Categories for user with id: {userId} not found.");
                }
            }

            return Ok(categories);
        }



        //[HttpGet("{userId}")]
        //public IActionResult GetCategoriesByUserId(int userId)
        //{
        //    var categories = _categoryService.GetCategoriesByUserId(userId);
        //    return Ok(categories);
        //}
    }
}


/*
 * Define a handler method which will create a category by reading the
 * Serialized category object from request body and save the category in
 * category table in database. Please note that the careatorId has to be unique
 * and the userID should be taken as the categoryCreatedBy for the
 * category. This handler method should return any one of the status messages
 * basis on different situations: 1. 201(CREATED - In case of successful
 * creation of the category 2. 409(CONFLICT) - In case of duplicate categoryId
 * 
 *  * This handler method should map to the URL "/api/category" using HTTP POST method
/*


 * Define a handler method which will delete a category from a database.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the category deleted successfully from
 * database. 2. 404(NOT FOUND) - If the category with specified categoryId is
 * not found. 
 * 
 * This handler method should map to the URL "/api/category/{id}" using HTTP Delete
 * method" where "id" should be replaced by a valid categoryId without {}
 */

/*
 * Define a handler method which will update a specific category by reading the
 * Serialized object from request body and save the updated category details in
 * a category table in database handle CategoryNotFoundException as well. please
 * note that the loggedIn userID should be taken as the categoryCreatedBy for
 * the category. This handler method should return any one of the status
 * messages basis on different situations: 1. 200(OK) - If the category updated
 * successfully. 2. 404(NOT FOUND) - If the category with specified categoryId
 * is not found. 
 * 
 * This handler method should map to the URL "/api/category/{id}" using HTTP PUT
 * method.
 */

/*
 * Define a handler method which will get us the category by a userId.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the category found successfully. 
 * 
 * This handler method should map to the URL "/api/category/{userId}" using HTTP GET method
 */

