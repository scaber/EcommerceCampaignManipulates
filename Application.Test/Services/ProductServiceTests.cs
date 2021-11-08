using Application.Interfaces;
using Application.Services;
using Xunit;

namespace Application.Test.Services
{
    public class ProductServiceTests
    {
        private readonly IProductService productService;
        public ProductServiceTests()
        {
            productService = new ProductService();
        }
        [Fact]
        public void Product_Added()
        {
            productService.Create("p2", 100, 1000);
            var product = productService.GetProducts("p2");

            Assert.Equal("p2", product.ProductCode);
            Assert.Equal(100, product.Price);
            Assert.Equal(1000, product.Stock);

        }
    }
}
