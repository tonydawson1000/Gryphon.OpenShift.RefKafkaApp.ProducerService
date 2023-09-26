using AutoMapper;
using Producer.Api.Models;

namespace Producer.Api.AutoMapperProfiles
{
    public class ProducerDeliveryExceptionProfile : Profile
    {
        public ProducerDeliveryExceptionProfile()
        {
            CreateMap<Confluent.Kafka.ProduceException<String, String>, ProducerDeliveryExceptionResult>()
                .ForMember(x => x.Key, opt => opt.MapFrom(o => o.DeliveryResult.Key))
                .ForMember(x => x.Value, opt => opt.MapFrom(o => o.DeliveryResult.Value))
                .ForMember(x => x.Status, opt => opt.MapFrom(o => o.DeliveryResult.Status));
        }
    }
}