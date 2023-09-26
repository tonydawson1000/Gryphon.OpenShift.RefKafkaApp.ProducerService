namespace Producer.Api.Models
{
    public class ProducerDeliveryExceptionResult
    {
        public required string Message { get; set; }

        public required string Key { get; set; }
        public required string Value { get; set; }
        public required string Status { get; set; }
    }
}