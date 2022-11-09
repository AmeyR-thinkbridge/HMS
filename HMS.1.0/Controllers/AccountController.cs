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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel signUpViewModel)
        {
            if (signUpViewModel.IsCustomer)
            {
                var result = await _userService.CreateUserAsync(signUpViewModel);
                if (result.HasError)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }

            if (!signUpViewModel.IsCustomer)
            {
                var result = await _userService.CreateUserAsync(signUpViewModel);
                if (result.HasError)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }




            return BadRequest();
        }
    }
}
