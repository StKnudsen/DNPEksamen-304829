using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Toy
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public bool IsFavorite { get; set; }
    }
}