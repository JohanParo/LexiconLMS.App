using AutoMapper;
using LexiconLMS.App.Client.DTOs;

namespace LexiconLMS.App.Client.Services
{
    public class MapperService
    {
        private readonly IMapper _mapper;
        public MapperService(IMapper mapper) => _mapper = mapper;

        public ModuleDto ModuleMap(ModuleDto module) => _mapper.Map<ModuleDto>(module);
        public CourseDto CourseMap(CourseDto course) => _mapper.Map<CourseDto>(course);

        public ActivityDto ActivityMap(ActivityDto activity) => _mapper.Map<ActivityDto>(activity);
    }
}


