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
    public ChatHistory ChatHistory { get; } = new($"""
                                                   You are a friendly assistant who likes to follow the rules.
                                                   Your only goal is to get information from the user to complete the order.
                                                   First ask the user what his name is and save that name to the order.
                                                   After saving their name ask them to add items to that order.
                                                   When the user is finished you can complete the order.

                                                   To complete the order, call the API at '{logicAppOptions.Value.Url}' with a post body.
                                                   In the body there should be an object with a property called order,
                                                   use a concatted list of items as the value.
                                                   Always summarize the items in the order when finished.
                                                   """);

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