using Homework.Api.Models;
using Homework.Api.ProductProviders;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace Homework.Api.Tests
{
    [TestFixture]
    public class DummyJsonProductProviderTests
    {
        private DummyJsonProductProvider dummyJsonProductProvider;
        private Mock<HttpMessageHandler> mockHttpMessageHandler;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://dummyjson.com")
            };

            dummyJsonProductProvider = new DummyJsonProductProvider(_httpClient);
        }

        [Test]
        public async Task GetProductsAsync_ShouldReturnsExactListOfProducts_AndNumberOfProduct()
        {
            //Arrange
            var products = new List<DummyJsonProduct>
            {
                new DummyJsonProduct {Id = 1, Brand ="Brand1", DiscountPercentage = 19.0, Price = 100.0, Title = "Title1", Rating = 4.9},
                new DummyJsonProduct {Id = 2, Brand ="Brand2", DiscountPercentage = 10.0, Price = 110.0, Title = "Title2", Rating = 3.9},
                new DummyJsonProduct {Id = 3, Brand ="Brand3", DiscountPercentage = 11.0, Price = 120.0, Title = "Title3", Rating = 4.2}
            };

            var responseContent = JsonSerializer.Serialize(new { products });
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, System.Text.Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            //Act
            var actualProducts = await dummyJsonProductProvider.GetProductsAsync();

            //Asserts
            Assert.That(actualProducts, Is.Not.Null);
            Assert.That(actualProducts.Count(), Is.EqualTo(3));
            Assert.That(actualProducts.First().Brand, Is.EqualTo("Brand1"));
        }

        [Test]
        public async Task GetProductsAsync_ShouldReturnsOnlyMatchedDiscountPercentageProducts()
        {
            //Arrange
            var products = new List<DummyJsonProduct>
            {
                new DummyJsonProduct {Id = 1, Brand ="Brand1", DiscountPercentage = 9.0, Price = 100.0, Title = "Title1", Rating = 4.9},
                new DummyJsonProduct {Id = 2, Brand ="Brand2", DiscountPercentage = 1.0, Price = 110.0, Title = "Title2", Rating = 3.9},
                new DummyJsonProduct {Id = 3, Brand ="Brand3", DiscountPercentage = 11.0, Price = 120.0, Title = "Title3", Rating = 4.2},
                new DummyJsonProduct {Id = 4, Brand ="Brand4", DiscountPercentage = 12.0, Price = 150.0, Title = "Title4", Rating = 4.1}
            };

            var responseContent = JsonSerializer.Serialize(new { products });
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, System.Text.Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            //Act
            var actualProducts = await dummyJsonProductProvider.GetProductsAsync();

            //Asserts
            Assert.That(actualProducts, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actualProducts.Count(), Is.EqualTo(2));
                Assert.That(actualProducts.First().Brand, Is.EqualTo("Brand3"));
            });
        }
    }
}