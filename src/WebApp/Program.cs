using Microsoft.SemanticKernel;
using WebApp.Configuration;
using WebApp.Plugins;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

var azureOpenAiConfiguration = builder.Configuration
    .GetSection(AzureOpenAiConfiguration.SectionKey)
    .Get<AzureOpenAiConfiguration>()!;

var kernelBuilder = builder.Services.AddKernel();
kernelBuilder.AddAzureOpenAIChatCompletion(
    azureOpenAiConfiguration.DeploymentName,
    azureOpenAiConfiguration.Endpoint,
    azureOpenAiConfiguration.ApiKey);
kernelBuilder.Plugins.AddFromType<AppearancePlugin>();

builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddSingleton<IAppearanceService, AppearanceService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
