using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class MaintenanceHistory
    {
        public int Id { get; set; }
        [Required]

        public string Description { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime DateCompleted { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int UserProfileId { get; set; }

    }
}
