using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Zip { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(30)]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
