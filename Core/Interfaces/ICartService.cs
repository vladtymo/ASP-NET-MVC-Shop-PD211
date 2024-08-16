using Core.Dtos;
using Data.Entities;
using System.Xml.Serialization;

namespace Core.Services
{
    public interface ICartService
    {
        List<ProductDto> GetProducts();
        List<Product> GetProductsEntity();
        int GetCount();
        void Add(int id);
        void Remove(int id);
        void Clear();
    }
}
