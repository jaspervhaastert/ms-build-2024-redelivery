using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using WebApp.Configuration;

namespace WebApp.Services;

public class ChatService(
    Kernel kernel,
    IChatCompletionService chatCompletionService,
    IOptions<LogicAppConfiguration> logicAppOptions) : IChatService
{
    public ChatHistory ChatHistory { get; } = [];

    private static readonly OpenAIPromptExecutionSettings PromptExecutionSettings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    };

    public async Task<(string? Response, string? Error)> SendMessageAsync(string message)
    {
        try
        {
            ChatHistory.AddUserMessage(message);

            var result = await chatCompletionService.GetChatMessageContentAsync(
                ChatHistory,
                PromptExecutionSettings,
                kernel);
            ChatHistory.AddAssistantMessage(result.Content!);

            return (result.Content, null);
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