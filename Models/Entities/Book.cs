using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoresAPI.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public string Id { get; set; }
        public string? IBSN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public Press? Press { get; set; }
    }
}
