namespace CodeMechanic.RazorHAT.Services;

public class LocalLoggingSettings
{
    public TimeSpan ExpiresIn { get; set; } = TimeSpan.FromDays(30);
}