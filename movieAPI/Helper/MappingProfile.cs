using AutoMapper;

namespace movieAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<movie,GetDTO>().ReverseMap();
            CreateMap<MovieDTO, movie>().ForMember(b => b.Poster, a => a.Ignore());

        }
    }
}
