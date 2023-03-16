using AutoMapper;
using LexiconLMS.App.Client.DTOs;

namespace LexiconLMS.App.Client.Mappings
{
    public class LmsMappings : Profile
    {
        public LmsMappings()
        {
            CreateMap<ModuleDto, ModuleDto>();
            CreateMap<CourseDto, CourseDto>();
            CreateMap<ActivityDto, ActivityDto>();
        }
    }
}
