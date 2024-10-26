using Homework.Api.Models;

namespace Homework.Api.Interfaces
{
    public interface IProductProvider
    {
        Uri BaseUri { get; }
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
