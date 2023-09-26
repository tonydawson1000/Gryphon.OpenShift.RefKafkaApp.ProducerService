using AutoMapper;
using Producer.Api.KafkaProducerConfig;

namespace Producer.Api.AutoMapperProfiles
{
    public class ProducerConfigProfile : Profile
    {
        public ProducerConfigProfile()
        {
            CreateMap<ProducerConfigOptions, Confluent.Kafka.ProducerConfig>();
        }
    }
}