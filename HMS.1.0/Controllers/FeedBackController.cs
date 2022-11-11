using Hms.Models;
using Hms.Models.ViewModels;
using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService;

        public FeedBackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        //Todo : Add proper routes to all apis'

        [HttpPost("{userId}/Add")]
        public async Task<IActionResult> AddFeedback([FromRoute] string userId, [FromBody] FeedBackViewModel feedBackViewModel)
        {
            var result = await _feedBackService.AddFeedBackAsync(userId, feedBackViewModel);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("FeedBacks")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _feedBackService.GetAllFeedBacks();
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("FeedBack/{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var result = await _feedBackService.GetFeedBackbyID(id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
        
        //Todo : Move Id and UserId into feedBackViewModel [fromBody]

        [HttpPut("Update/{id}/{userId}")]
        public async Task<IActionResult> UpdateFeedBack([FromRoute] int id, [FromRoute] string userId, [FromBody] FeedBackViewModel feedBackViewModel)
        {
            var result = await _feedBackService.UpdateFeedBackAsync(id, userId, feedBackViewModel);
            if (result)
            {
                return Ok(feedBackViewModel);
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFeedBack([FromRoute] int id)
        {
            var feedBack = await _feedBackService.GetFeedBackbyID(id);
            var result = _feedBackService.Delete(feedBack);
            if (result)
            {
                return Ok($"Your feedback is deleted");
            }
            return BadRequest();
        }

        [HttpGet("{userId}/FeedBacks")]
        public async Task<IActionResult> GetFeedBackByUserId([FromRoute] string userId)
        {
            var result = await _feedBackService.GetFeedBacksByUserId(userId);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
