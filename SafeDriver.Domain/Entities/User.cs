using System;
using System.ComponentModel.DataAnnotations;

namespace SafeDriver.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
    
}