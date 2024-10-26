using System.Text.Json.Serialization;

namespace Homework.Api.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Brand { get; set; }
        public double? Price { get; set; }
        public double? DiscountPercentage { get; set; }
        public double? Rating { get; set; }
    }
}
