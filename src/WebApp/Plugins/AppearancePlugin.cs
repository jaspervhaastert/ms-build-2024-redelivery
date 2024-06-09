using System.ComponentModel;
using Microsoft.SemanticKernel;
using WebApp.Services;

namespace WebApp.Plugins;

public class AppearancePlugin(IAppearanceService appearanceService)
{
    [KernelFunction, Description("Enables or disables dark mode.")]
    public void SetDarkMode(bool enabled) => appearanceService.DarkModeEnabled = enabled;
}