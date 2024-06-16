using Microsoft.SemanticKernel.ChatCompletion;

namespace WebApp.Services;

public interface IChatService
{
    public ChatHistory ChatHistory { get; }

    public Task<(string? Response, string? Error)> SendMessageAsync(string message);
}