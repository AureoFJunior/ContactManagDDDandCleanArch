using System.ComponentModel.DataAnnotations;

namespace ContactManag.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public short? IsLogged { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}