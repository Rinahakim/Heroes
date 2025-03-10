using Heroes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Heroes.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> Signup(SignupModel signupModel) {
            AppUser user = new()
            {
                UserName = signupModel.Email, 
                Id = Guid.NewGuid().ToString(), 
                Email = signupModel.Email,
                Heroes = new List<HeroModel>()
            };
     
            var res = await _userManager.CreateAsync(user, signupModel.Password);
            if (res.Succeeded) {
                return "signup successful";
            }
            foreach (var error in res.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }

            return null;
        }

        public async Task<string> Login(LoginModel loginModel) {
            var res = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);
            if (!res.Succeeded) {
                return null;
            }
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(AppUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //this key for to ensure that the token not chance 
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"], //who created the token
                audience: _configuration["JWT:ValidAudience"], //who can use this token
                expires: DateTime.Now.AddDays(1), //it will expire after day
                claims: authClaims, // the data that include the token
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature) //Cryptographic signature on token
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
