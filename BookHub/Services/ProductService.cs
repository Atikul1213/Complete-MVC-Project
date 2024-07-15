using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Repository;

namespace BookHub.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.Getall();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.Get(c => c.Id == id);
        }

        public void AddProduct(Product product)
        {
            _productRepository.Add(product);
        }

        public void UpdateProduct(Product product)
        {

            _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.Get(c => c.Id == id);
            _productRepository.Remove(product);
        }
    }
}
