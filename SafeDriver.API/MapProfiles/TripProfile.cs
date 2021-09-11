using AutoMapper;
using SafeDriver.API.Models.InputModels;
using SafeDriver.API.Models.OutputModels;
using SafeDriver.Domain.Entities;

namespace SafeDriver.API.MapProfiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Trip, GetTripInfoOutputModel>();
        }
    }
}