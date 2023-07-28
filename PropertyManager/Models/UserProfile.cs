using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool IsEmployee { get; set; }

    }
}
