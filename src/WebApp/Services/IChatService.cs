namespace WebApp.Services;

public interface IChatService
{
    public Task<(string? Response, string? Error)> SendMessageAsync(ICollection<(string, string)> messages);
}