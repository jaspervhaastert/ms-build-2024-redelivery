namespace WebApp.Services;

public interface IAppearanceService
{
    public bool DarkModeEnabled { get; set; }
    public string? Class { get; }
}