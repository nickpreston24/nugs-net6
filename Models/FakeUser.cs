namespace nugsnet6.Models;

public sealed class FakeUser
{
    public string Id { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Basic;
}
