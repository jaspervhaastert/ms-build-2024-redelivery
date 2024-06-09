using System.Net;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace WebApp.Services;

public class ChatService(Kernel kernel) : IChatService
{
    private static readonly KernelArguments KernelArguments = new(new OpenAIPromptExecutionSettings
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    });

    public async Task<(string? Response, string? Error)> SendMessageAsync(ICollection<(string, string)> messages)
    {
        try
        {
            var promptMessages = messages.Select(m => $"<message role=\"{m.Item1}\">{m.Item2}</message>");
            var prompt = string.Join('\n', promptMessages);

            var result = await kernel.InvokePromptAsync(prompt, KernelArguments);
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