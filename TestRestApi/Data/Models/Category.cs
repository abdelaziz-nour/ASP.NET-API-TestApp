using System.ComponentModel.DataAnnotations;

namespace TestRestApi.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string? notes { get; set; }
        public List<Item> items { get; set; }

    }
}
