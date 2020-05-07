using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Address
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int HouseNumber { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
