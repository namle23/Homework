using Homework.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Api.Controllers
{
    /// <summary>
    /// Return list of product
    /// </summary>
    /// <param name="productService"></param>
    [ApiController]
    [Route("api/products")]
    public class ProductsController (ProductService productService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var products = await productService.GetProductsAsync();

                return Ok(products);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
