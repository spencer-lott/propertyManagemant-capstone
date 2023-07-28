using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PropertyManager.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Type { get; set; }

        public string SizeDescription { get; set; }

        public int Rent { get; set; }

        [Required]
        public bool Vacant { get; set; }

        public Tenant? Tenant { get; set; }

    }
}



