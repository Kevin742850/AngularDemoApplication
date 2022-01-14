using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(100)]
        public string Zip { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string SSN { get; set; } = string.Empty;

        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(30)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [Required]
        public int PharmacyId { get; set; }

        public Pharmacy? Pharmacy { get; set; }
    }
}
