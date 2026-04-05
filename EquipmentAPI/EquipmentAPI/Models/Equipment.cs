using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentAPI.Models
{
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }

        // EF Core requires a parameterless constructor
        public Equipment() { }
    }
}
