namespace Producer.Api.KafkaProducerConfig
{
    public class ProducerConfigOptions
    {
        public required string TopicName { get; set; }

        public required string BootstrapServers { get; set; }
        public required string ClientId { get; set; }

        public required string Acks { get; set; }
        public required int MessageTimeoutMs { get; set; }
        public required int BatchNumMessages { get; set; }
        public required int LingerMs { get; set; }
        public required string CompressionType { get; set; }

        public required bool EnableIdempotence { get; set; }

        public required int MessageSendMaxRetries { get; set; }
        public required int MaxInFlight { get; set; }
    }
}