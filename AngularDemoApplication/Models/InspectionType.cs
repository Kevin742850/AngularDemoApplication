using System.ComponentModel.DataAnnotations;

namespace AngularDemoApplication.Models
{
    public class InspectionType
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = String.Empty;
    }

}
