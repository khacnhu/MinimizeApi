using MinimizeApi.Models.Dtos;
using MinimizeApi.Repositories;

namespace MinimizeApi.Services
{
    public class ProductService(IProductRepo productRepo) : IProductService
    {
        public async Task<Response> Add(AddRequestDTO request)
        {
            return await productRepo.Add(request);
        }

        public async Task<Response> Delete(int id)
        {
            return await productRepo.Delete(id);
        }

        public async Task<List<ResponseDTO>> GetAll()
        {
            return await productRepo.GetAll();
        }

        public async Task<ResponseDTO> GetById(int id)
        {
            return await productRepo.GetById(id);
        }

        public async Task<Response> Update(UpdateRequestDTO request, int id)
        {
            return await productRepo.Update(request, id);
        }
    }
}
