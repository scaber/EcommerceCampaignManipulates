using Domain;

namespace Application.Interfaces
{
    public interface IProductService
    {
        void Create(string productCode, double price, int stock);
        Products GetProducts(string productCode);
        void IncraseTime(int totalIncrase); 
    }
}
