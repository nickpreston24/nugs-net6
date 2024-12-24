using Microsoft.AspNetCore.Html;

namespace nugsnet6;

public class CustomModal
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public HtmlString Render { get; set; } = HtmlString.Empty;

    public virtual string BadgeIcon { get; set; } = "[!]"; //TODO: This is just a default. Allow override with svg.

    // public string BadgeCSS { get; set; } = AlertType.Success.ToString();
    // For avatar/icons like Warning [!].  For now, just the 'alerttype', b/c it works
}
