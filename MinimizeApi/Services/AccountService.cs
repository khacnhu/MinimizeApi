using MinimizeApi.Models.Dtos;

namespace MinimizeApi.Services
{
    public class AccountService(IAccountRepo accountRepo) : IAcountService
    {
        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {
            return await accountRepo.Login(loginDTO);
        }

        public async Task<Response> Register(RegisterDTO registerDTO)
        {
            return await accountRepo.Register(registerDTO);
        }


    }
}
