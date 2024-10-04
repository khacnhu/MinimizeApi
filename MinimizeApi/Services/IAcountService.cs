using MinimizeApi.Models.Dtos;

namespace MinimizeApi.Services
{
    public interface IAcountService
    {
        Task<Response> Register(RegisterDTO registerDTO);
        Task<LoginResponse> Login(LoginDTO loginDTO);

    }
}
