using AutoMapper;
using SafeDriver.API.Models.InputModels;
using SafeDriver.API.Models.OutputModels;
using SafeDriver.Domain.Entities;

namespace SafeDriver.API.MapProfiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<CreateDriverInputModel, Driver>();
            CreateMap<Driver, GetDriverInfoOutputModel>();
        }
    }
}