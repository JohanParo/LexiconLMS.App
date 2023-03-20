using AutoMapper;
using LexiconLMS.App.Server.Models;
using LexiconLMS.App.Shared;

namespace LexiconLMS.App.Server.Mappings
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<Activity,ActivityDto>()
                .ForMember(
                dest => dest.ActivityTypeType,
                from => from.MapFrom(a => a.ActivityType.Type))
                ;
            CreateMap<Module, ModuleDto>();
            CreateMap<Course, CourseDto> ();
            CreateMap<ActivityType, ActivityTypeDto>();
            CreateMap<ApplicationUser, ApplicationUserDto>();

            CreateMap<StudentCourseDto, CourseDto>()
           .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules.Where(m => m.Published)))
           .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules.Select(m =>
               new ModuleDto
               {
                   Id = m.Id,
                   Title = m.Title,
                   Description = m.Description,
                   StartTime = m.StartTime,
                   EndTime = m.EndTime,
                   CourseId = m.CourseId,
                   Published = m.Published,
                   Activities = m.Activities.Where(a => a.Published).ToList()
               }).Where(m => m.Published)));

            CreateMap<CourseDto, StudentCourseDto>();
        }
    }
}
