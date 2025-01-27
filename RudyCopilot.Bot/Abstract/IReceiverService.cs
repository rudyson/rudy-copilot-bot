namespace RudyCopilot.Bot.Abstract;
public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken cancellationToken = default);
}