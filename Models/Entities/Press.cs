using System.ComponentModel.DataAnnotations;

namespace BookStoresAPI.Models
{
    public class Press
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
