using System;
using System.ComponentModel.DataAnnotations;

namespace SafeDriver.Domain.Entities {

    public class Driver : User {
        public string DriverUUID { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        [Required]
        public string DriversLicenseNumber { get; set; }

        [Required]
        public DateTime DriverLicenseExpireDate { get; set; }
        public bool IsProfessionalDriver { get; set; }

        [Required]
        public string AutomotiveInsuranceProvider { get; set; }
        public int Score { get; set; }
    }
}