using MinimizeApi.Models.Dtos;

namespace MinimizeApi.Repositories
{
    public interface IProductRepo
    {
        Task<Response> Add(AddRequestDTO request);

        Task<Response> Update(UpdateRequestDTO request, int id);

        Task<List<ResponseDTO>> GetAll();

        Task<ResponseDTO> GetById(int id);
        Task<Response> Delete(int id);


    }
}
