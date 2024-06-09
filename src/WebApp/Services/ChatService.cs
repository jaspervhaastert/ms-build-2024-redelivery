using System.Net;
using Microsoft.SemanticKernel;

namespace WebApp.Services;

public class ChatService(Kernel kernel) : IChatService
{
    public async Task<(string? Response, string? Error)> SendMessageAsync(ICollection<(string, string)> messages)
    {
        try
        {
            var promptMessages = messages.Select(m => $"<message role=\"{m.Item1}\">{m.Item2}</message>");
            var prompt = string.Join('\n', promptMessages);

            var result = await kernel.InvokePromptAsync(prompt);
            return (result.GetValue<string>(), null);
        }
        catch (HttpOperationException e) when (e.StatusCode == HttpStatusCode.TooManyRequests)
        {
            return (null, "Too many requests");
        }
        catch (Exception e)
        {
            return (null, e.Message);
        }
    }
}