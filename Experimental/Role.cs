using CodeMechanic.Types;

namespace nugsnet6.Models;

public class Role : Enumeration
{
    public static Role Admin => new(1, nameof(Admin).ToLower());
    public static Role Basic => new(2, nameof(Basic).ToLower());

    public Role(int id, string name)
        : base(id, name) { }
}
