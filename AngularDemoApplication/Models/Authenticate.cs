using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models
{
    public class Authenticate
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public bool IsActive { get; set; } = true;

    }
}
