using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimizeApi.Data;
using MinimizeApi.Models.Dtos;
using MinimizeApi.Models.Entities;

namespace MinimizeApi.Repositories
{
    public class ProductRepo(IMapper mapper, AppDbContext appDbContext) : IProductRepo
    {
        public async Task<Response> Add(AddRequestDTO request)
        {
            appDbContext.Products.Add(mapper.Map<Product>(request));
            await appDbContext.SaveChangesAsync();
            return new Response(true, "SAVE");
       }

        public async Task<Response> Delete(int id)
        {
            appDbContext.Products.Remove(await appDbContext.Products.FindAsync(id));
            await appDbContext.SaveChangesAsync();
            return new Response(true, "Deleted");

        }

        public async Task<List<ResponseDTO>> GetAll()
        {
            return mapper.Map<List<ResponseDTO>>(await appDbContext.Products.ToListAsync());
        }

        public async Task<ResponseDTO> GetById(int id)
        {
            return mapper.Map<ResponseDTO>(await appDbContext.Products.FindAsync(id));
        }

        public async Task<Response> Update(UpdateRequestDTO request, int id)
        {
            Product product = await appDbContext.Products.FindAsync(id);
            var updateProductDTO = new UpdateRequestDTO(product.Name, product.Description,product.Price ,product.Quantity);

            if (product != null)
            {
                product.Name = updateProductDTO.Name;
                product.Price = updateProductDTO.Price;
                product.Description = updateProductDTO.Description;
                product.Quantity = updateProductDTO.Quantity;
                await appDbContext.SaveChangesAsync();
                return new Response(true, "Updated");

            }
            return new Response(false, "FAIELD TO UPDATE");


        }


    }
}
