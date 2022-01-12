using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
