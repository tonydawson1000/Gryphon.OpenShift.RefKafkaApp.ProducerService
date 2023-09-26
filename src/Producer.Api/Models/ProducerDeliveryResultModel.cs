namespace Producer.Api.Models
{
    public class ProducerDeliveryResultModel
    {
        public required string Status { get; set; }

        public required string Key { get; set; }
        public required string Value { get; set; }

        public required string Topic { get; set; }
        public required int Partition { get; set; }
        public required long Offset { get; set; }
    }
}