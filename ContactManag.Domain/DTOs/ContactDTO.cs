using System.ComponentModel.DataAnnotations;

namespace ContactManag.Domain.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int PersonId { get; set; }
    }
}