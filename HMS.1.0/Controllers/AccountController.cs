using Hms.Models.ViewModels;
using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //Todo : Generic todo fix naming convention. 
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel signUpViewModel)
        {

            var result = await _userService.AddUserAsync(signUpViewModel);
            if (result.HasError)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
    }
}
