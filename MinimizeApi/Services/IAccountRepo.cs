using MinimizeApi.Models.Dtos;

namespace MinimizeApi.Services
{
    public interface IAccountRepo
    {
        Task<Response> Register(RegisterDTO registerDTO);
        Task<LoginResponse> Login(LoginDTO loginDTO);
    }
}
