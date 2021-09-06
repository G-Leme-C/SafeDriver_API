using System;

namespace SafeDriver.API.Models.InputModels
{
    public class CreateDriverInputModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DriversLicenseNumber { get; set; }
        public DateTime DriverLicenseExpireDate { get; set; }
        public bool IsProfessionalDriver { get; set; }
        public string AutomotiveInsuranceProvider { get; set; }
    }
}