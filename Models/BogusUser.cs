using Bogus.DataSets;

namespace nugsnet6.Models;

public class BogusUser
{
    public BogusUser(int i, string replace) { }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }
    public string Avatar { get; set; }
    public string Email { get; set; }
    public string SomethingUnique { get; set; }
    public string FullName { get; set; }
    public Name.Gender Gender { get; set; }
    public int Id { get; set; }
}
