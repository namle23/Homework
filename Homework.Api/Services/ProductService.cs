using Homework.Api.Interfaces;
using Homework.Api.Models;

namespace Homework.Api.Services
{
    public class ProductService(IEnumerable<IProductProvider> productProviders)
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var tasks = productProviders.Select(productProvider =>  productProvider.GetProductsAsync());
            var results = await Task.WhenAll(tasks);

            return results.SelectMany(result => result);
        }
    }
}
