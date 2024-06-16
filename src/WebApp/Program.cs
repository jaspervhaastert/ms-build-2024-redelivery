using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;
using WebApp.Configuration;
using WebApp.Plugins;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

var azureOpenAiConfiguration = builder.Configuration
    .GetSection(AzureOpenAiConfiguration.SectionKey)
    .Get<AzureOpenAiConfiguration>()!;

var logicAppConfigurationSection = builder.Configuration.GetRequiredSection(LogicAppConfiguration.SectionKey);
builder.Services.Configure<LogicAppConfiguration>(logicAppConfigurationSection);

var kernelBuilder = builder.Services.AddKernel();
kernelBuilder.AddAzureOpenAIChatCompletion(
    azureOpenAiConfiguration.DeploymentName,
    azureOpenAiConfiguration.Endpoint,
    azureOpenAiConfiguration.ApiKey);
kernelBuilder.Plugins.AddFromType<AppearancePlugin>();
kernelBuilder.Plugins.AddFromType<OrderPlugin>();
kernelBuilder.Plugins.AddFromType<HttpPlugin>();

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
