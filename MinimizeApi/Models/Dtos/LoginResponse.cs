namespace MinimizeApi.Models.Dtos
{
    public record LoginResponse
    (
        bool Flag,
        string Token = null!,
        string Message = null!

    );
}
