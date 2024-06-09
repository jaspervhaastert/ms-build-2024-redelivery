using Microsoft.SemanticKernel;
using WebApp.Configuration;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

var azureOpenAiConfiguration = builder.Configuration
    .GetSection(AzureOpenAiConfiguration.SectionKey)
    .Get<AzureOpenAiConfiguration>()!;

builder.Services
    .AddKernel()
    .AddAzureOpenAIChatCompletion(
        azureOpenAiConfiguration.DeploymentName,
        azureOpenAiConfiguration.Endpoint,
        azureOpenAiConfiguration.ApiKey);

builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
