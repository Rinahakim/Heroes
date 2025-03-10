using Heroes.Models;

namespace Heroes.Repositories
{
    public interface IAccountRepository
    {
        Task<string> Signup(SignupModel signupModel);
        Task<string> Login(LoginModel loginModel);
    }
}