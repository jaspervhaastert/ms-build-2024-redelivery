namespace WebApp.Services;

public class AppearanceService : IAppearanceService
{
    public bool DarkModeEnabled { get; set; }
    public string? Class => DarkModeEnabled ? "dark" : null;
}