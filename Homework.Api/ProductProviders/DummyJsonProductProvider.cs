using Homework.Api.Interfaces;
using Homework.Api.Models;
using Homework.Api.Models.Responses;

namespace Homework.Api.ProductProviders
{
    public class DummyJsonProductProvider : IProductProvider
    {
        private HttpClient _httpClient;
        public Uri BaseUri { get; } = new Uri("https://dummyjson.com");

        public DummyJsonProductProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = BaseUri;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"/products");

            var data = await response.Content.ReadFromJsonAsync<DummyJsonProductResponse>();

            return data?.Products?
                .Where(product => product.DiscountPercentage >= 10)
                .Select(product => new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Brand = product.Brand,
                    Price = product.Price,
                    DiscountPercentage = product.DiscountPercentage,
                    Rating = product.Rating
                }
                ) ?? new List<Product>();
        }
    }
}
