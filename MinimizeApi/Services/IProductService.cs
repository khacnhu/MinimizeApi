using MinimizeApi.Models.Dtos;

namespace MinimizeApi.Services
{
    public interface IProductService
    {
        Task<Response> Add(AddRequestDTO request);

        Task<Response> Update(UpdateRequestDTO request, int id);

        Task<List<ResponseDTO>> GetAll();

        Task<ResponseDTO> GetById(int id);
        Task<Response> Delete(int id);

    }
}
