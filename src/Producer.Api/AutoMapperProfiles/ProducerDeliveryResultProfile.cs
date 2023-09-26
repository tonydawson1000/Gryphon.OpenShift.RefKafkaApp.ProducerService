using AutoMapper;
using Producer.Api.Models;

namespace Producer.Api.AutoMapperProfiles
{
    public class ProducerDeliveryResultProfile : Profile
    {
        public ProducerDeliveryResultProfile()
        {
            CreateMap<Confluent.Kafka.DeliveryResult<String, String>, ProducerDeliveryResultModel>();
        }
    }
}