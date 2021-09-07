using System;
using System.ComponentModel.DataAnnotations;

namespace SafeDriver.Domain.Entities {

    public class Driver : User {
        public string DriverUUID { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DriversLicenseNumber { get; set; }
        public DateTime DriverLicenseExpireDate { get; set; }
        public bool IsProfessionalDriver { get; set; }
        public string AutomotiveInsuranceProvider { get; set; }

    }
}