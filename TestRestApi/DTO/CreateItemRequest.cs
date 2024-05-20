namespace TestRestApi.DTO
{
    public class CreateItemRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Notes { get; set; }
        public IFormFile? Image { get; set; }
        public int? CategoryId { get; set; }

    }
}
