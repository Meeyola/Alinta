using System;
using System.ComponentModel.DataAnnotations;

namespace Alinta.Model
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
