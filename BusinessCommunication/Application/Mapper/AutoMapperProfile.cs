using Domain.Dtos.Request;

namespace Application.Mapper
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SendMessageRequestDto, Domain.Models.Message>();
        }

    }
}
