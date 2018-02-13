
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using AutoMapper;

using Web.Data.GuestBook;
using Web.Data.GuestBook.Entities;
using Web.Models.GuestBook;
using System.Threading.Tasks;

namespace Web.Controllers.GuestBook
{
    [Route("api/guestbook/[Controller]")]
    public class MessagesController : Controller
    {
        private readonly IRepository _repo;
        private readonly ILogger<MessagesController> _logger;
        private readonly IMapper _mapper;


        public MessagesController(
            IRepository repo,
            ILogger<MessagesController> logger,
            IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }


        //------------------
        //  Get One Message

        [HttpGet("{id:int}", Name = "MessageGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var entity = _repo.GetMessage(id);
                if (entity != null)
                {
                    var dto = _mapper.Map<Message, MessageDto>(entity);
                    return Ok(dto);
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get message: {ex}");
                return BadRequest("Failed to get message");
            }
        }


        //-----------------------
        //  Get Messages

        [HttpGet(Name = "MessageSearch")]
        public IActionResult Get([FromQuery]MessageSearchDto search)
        {
            try
            {
                var result = new MessageResultsDto();

                // Default to first page
                if (search.Page < 1)
                    search.Page = 1;

                // Default to 20 records per page
                if (search.Size < 1)
                    search.Size = 20;

                result =
                    _repo.SearchMessages(search,
                        m =>
                        // By Category Id
                        (search.Cat == null || m.CategoryId == search.Cat)

                        // By Sender Name like
                        && (string.IsNullOrEmpty(search.Sender) || m.Name.Contains(search.Sender))

                        // By Sender Email
                        && (string.IsNullOrEmpty(search.Email) || m.Email == search.Email)

                        // By Subject
                        && (string.IsNullOrEmpty(search.Subject) || m.Subject.Contains(search.Subject))

                        // Only read or unread messages
                        && (search.Read == null ||
                            (search.Read == true && m.DateRead != null) ||
                            (search.Read == false && m.DateRead == null))

                        // Messages sent from certain date
                        && (search.From == null || m.DateSent.Date >= search.From.Value.Date)

                        // Messages sent upto certain date
                        && (search.To == null || m.DateSent.Date <= search.To.Value.Date)
                      );

                // TODO: Fix AutoMapper Error: Unmapped properties:  Category
                // var dtos = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(entities);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get messages: {ex}");
                return BadRequest("Failed to get messages");
            }
        }


        //------------------
        //  Add Message

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageDto dto)
        {
            // Add message to database
            try
            {
                if (ModelState.IsValid)
                {
                    //TODO: var entity = _mapper.Map<MessageDto, Message>(dto);  ..... Fix AutoMapper Error: Unmapped properties:  Category

                    var entity = new Message
                    {
                        Name = dto.Name,
                        Email = dto.Email,
                        Subject = dto.Subject,
                        Text = dto.Text,
                        DateSent = DateTime.UtcNow,
                        CategoryId = dto.CategoryId
                    };

                    _repo.Add(entity);

                    if (await _repo.SaveAllAsync())
                    {
                        // Map entity to Dto using AutoMapper
                        var messageDto = _mapper.Map<Message, MessageDto>(entity);

                        var messageUri = Url.Link("MessageGet", new { id = messageDto.Id });

                        return Created(messageUri, messageDto);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add a new message: {ex}");
            }
            return BadRequest("Failed to add a new message");
        }


        //------------------
        //  Update Message

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Message m)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _repo.GetMessage(id);
                    if (entity == null)
                        return NotFound($"Could not find a message with an Id of {id}");

                    // Map m to entity
                    entity.Name = m.Name ?? entity.Name;
                    entity.Subject = m.Subject ?? entity.Subject;
                    entity.Text = m.Text ?? entity.Text;
                    entity.CategoryId = m.CategoryId > 0 ? m.CategoryId : entity.CategoryId;
                    entity.DateSent = m.DateSent != DateTime.MinValue ? m.DateSent : entity.DateSent;
                    entity.DateRead = m.DateRead != DateTime.MinValue ? m.DateRead : entity.DateRead;

                    // Return response
                    if (await _repo.SaveAllAsync())
                    {
                        return Ok(entity);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex) {
                _logger.LogError($"Failed to update the message: {ex}");
            }
            return BadRequest("Could not update the message");
        }

        //------------------
        //  Delete Message

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = _repo.GetMessage(id);
                if (entity == null)
                    return NotFound($"Could not find message with ID of {id}");

                _repo.Delete(entity);
                if (await _repo.SaveAllAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex) { _logger.LogError($"Failed to delete message: {ex}"); }

            return BadRequest("Could not delete the message");
        }

    }
}
