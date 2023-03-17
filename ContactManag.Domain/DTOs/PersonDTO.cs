using System.ComponentModel.DataAnnotations;

namespace ContactManag.Domain.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}