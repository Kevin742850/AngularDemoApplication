using System.ComponentModel.DataAnnotations;

namespace AngularDemoApplication.Models
{
    public class Inspection
    {

        public int Id { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
        [StringLength(250)]
        public string Comments { get; set; } = string.Empty;

        public int InspectionTypeId { get; set; }
        public InspectionType? InspectionType { get; set; }

    }
}
