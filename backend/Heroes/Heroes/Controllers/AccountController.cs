using Heroes.Models;
using Heroes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var res = await _accountRepository.Signup(signupModel);
            if (string.IsNullOrWhiteSpace(res))
            {
                return Unauthorized(new { message = "Signup failed. Please check the input data." });
            }

            return Ok(new { message = res });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var token = await _accountRepository.Login(loginModel);
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Invalid login.");
            }

            return Ok(new { token });
        }
    }
}

