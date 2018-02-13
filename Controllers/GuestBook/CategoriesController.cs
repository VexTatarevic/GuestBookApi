
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Data.GuestBook;

namespace Web.Controllers.GuestBook
{
    [Route("api/guestbook/[Controller]")]
    public class CategoriesController : Controller
    {
        private readonly IRepository _repo;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IRepository repo, ILogger<CategoriesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }



        // Get Category List

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _repo.GetAllCategories();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get message categories: {ex}");
                return BadRequest("Failed to get message categories");
            }
        }


        // Get Category

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var entity = _repo.GetCategory(id);

                if (entity != null)
                    return Ok(entity);

                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get message category: {ex}");
                return BadRequest("Failed to get messag category");
            }
        }





    }
}
