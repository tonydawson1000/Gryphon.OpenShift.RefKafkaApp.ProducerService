using AutoMapper;
using Producer.Api.Models;

namespace Producer.Api.AutoMapperProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageToSendModel, Confluent.Kafka.Message<string, string>>()
                .ForMember(x => x.Timestamp, opt => opt.MapFrom(o => DateTime.UtcNow));
        }
    }
}