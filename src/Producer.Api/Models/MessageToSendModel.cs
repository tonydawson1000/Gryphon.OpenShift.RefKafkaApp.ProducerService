namespace Producer.Api.Models;

public class MessageToSendModel
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}