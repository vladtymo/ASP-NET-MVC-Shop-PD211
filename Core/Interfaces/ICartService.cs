using Core.Dtos;

namespace Core.Services
{
    public interface ICartService
    {
        List<ProductDto> GetProducts();
        int GetCount();
        void Add(int id);
        void Remove(int id);
    }
}
