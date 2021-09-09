using AutoMapper;
using SafeDriver.API.Models.InputModels;
using SafeDriver.API.Models.OutputModels;
using SafeDriver.Domain.Entities;

namespace SafeDriver.API.MapProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventInputModel, Event>();
            CreateMap<Event, GetDriverEventInfoOutputModel>();
        }
    }
}