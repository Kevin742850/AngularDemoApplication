using PharmacyManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Formula { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public bool IsControlDrug { get; set; } = false;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal PurchasePrice { get; set; } = 0.0M;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal SalePrice { get; set; } = 0.0M;
        [Required]
        public int ItemEachBox { get; set; }
        [StringLength(500)]
        public string PicturePath { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int StrengthId { get; set; }
        [Required]
        public int FormId { get; set; }

        public Category? Category { get; set; }
        public Company? Company { get; set; }

        public Strength? Strength { get; set; }
        public Form? Form { get; set; }

    }
}

