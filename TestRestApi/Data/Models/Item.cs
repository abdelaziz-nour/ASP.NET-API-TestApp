using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestRestApi.Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Notes { get; set; }
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public byte[]? Image { get; set; }


    }
}
