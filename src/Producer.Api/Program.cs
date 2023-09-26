using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Producer.Api.KafkaProducerConfig;
using Producer.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

builder.Services.Configure<ProducerConfigOptions>(builder.Configuration.GetSection("Kafka.ProducerConfig"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet(
    "/config",
    Results<NotFound, Ok<ProducerConfigOptions>> (
        IOptions<ProducerConfigOptions> producerConfigOptions) =>
    {
        return TypedResults.Ok(producerConfigOptions.Value);
    })
    .WithOpenApi()
    .WithSummary("What Kafka Config is setup.")
    .WithDescription("Review the Kafka Configuration setup for this Producer Client.");

app.MapPost(
    "/messages",
    async Task<Results<Ok<ProducerDeliveryResultModel>, BadRequest<ProducerDeliveryExceptionResult>>> (
        IOptions<ProducerConfigOptions> producerConfigOptions,
        IMapper mapper,
        ILogger<MessageToSendModel> logger,
        MessageToSendModel messageToSendModel) =>
    {
        var producerConfig = mapper.Map<Confluent.Kafka.ProducerConfig>(producerConfigOptions.Value);

        var kafkaMessage = mapper.Map<Confluent.Kafka.Message<String, String>>(messageToSendModel);

        var topicName = producerConfigOptions.Value.TopicName;

        logger.LogInformation($"About to Produce a Message to Kafka - Key = '{kafkaMessage.Key}' - Value = '{kafkaMessage.Value}' - Topic = '{topicName}'");

        ProducerDeliveryResultModel produceResult;
        ProducerDeliveryExceptionResult produceException;

        using (var producer = new Confluent.Kafka.ProducerBuilder<String, String>(producerConfig).Build())
        {
            try
            {
                var deliveryResult = await producer.ProduceAsync(topicName, kafkaMessage);

                logger.LogInformation($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");

                produceResult = mapper.Map<ProducerDeliveryResultModel>(deliveryResult);

                // Yes should return a '201' - but request/response vs pub/sub ...
                return TypedResults.Ok<ProducerDeliveryResultModel>(produceResult);
            }
            catch (Confluent.Kafka.ProduceException<String, String> ex)
            {
                logger.LogError($"Delivery failed : {ex.Error.Reason}");

                produceException = mapper.Map<ProducerDeliveryExceptionResult>(ex);

                // So wrong - most likly a mis-configuration
                return TypedResults.BadRequest(produceException);
            }
        }
    })
    .WithOpenApi()
    .WithSummary("'Publish' a Message to Kafka.")
    .WithDescription("Construct a Message (Key and Value), Construct a ProducerClient (using Config) and 'Publish' the Message to Kafka");

app.Run();
