
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoMapper;

using Web.Data.GuestBook;
using Web.Data.GuestBook.Entities;
using Web.Models.GuestBook;
using System.Linq;

namespace Web.Controllers.GuestBook
{
    [Route("api/guestbook/category/{categoryid}/messages")]
    public class CategoryMessagesController : Controller
    {
        private readonly IRepository _repo;
        private readonly ILogger<CategoryMessagesController> _logger;
        private readonly IMapper _mapper;

        public CategoryMessagesController(
            IRepository repo, 
            ILogger<CategoryMessagesController> logger,
            IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }


        // Get Category

        [HttpGet]
        public IActionResult Get(int categoryId)
        {
            try
            {
                var cat = _repo.GetCategory(categoryId, true);

                if (cat != null)
                {
                    var messageDtos =_mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(cat.Messages);
                    
                    return Ok(messageDtos);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get category: {ex}");
                return BadRequest("Failed to get category");
            }
        }


        // Get Message in Category

        [HttpGet("{id}")]
        public IActionResult Get(int categoryId, int id)
        {
            try
            {
                var cat = _repo.GetCategory(categoryId, true);

                if (cat != null)
                {
                    var message = cat.Messages.Where(m => m.Id == id).FirstOrDefault();
                    if (message != null)
                    {
                        var messageDto = _mapper.Map<Message,MessageDto>(message);
                        return Ok(messageDto);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get message for the given category: {ex}");
                return BadRequest("Failed to get message for the given category");
            }
        }





    }
}
