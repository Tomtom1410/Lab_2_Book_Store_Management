using System.ComponentModel.DataAnnotations;

namespace BookStoreClient.Models
{
    public class BookDto
    {
        public string? Id { get; set; }
        public string? IBSN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public PressDto? Press { get; set; }
    }
}
