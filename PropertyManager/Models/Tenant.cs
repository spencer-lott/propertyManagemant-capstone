using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PropertyManager.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Employment { get; set; }

        [Required]
        public string EmergencyContactName { get; set; }

        [Required]
        public string EmergencyContactPhone { get; set; }

        public string GeneralNotes { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        public Property? Property { get; set; }

    }
}
