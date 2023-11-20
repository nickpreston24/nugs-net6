using nugsnet6;
using nugsnet6.Models;
using TPOT_Links.Controllers;


public class PartService : IPartService
{
    public List<Part> ReadAll()
    {
        return new List<Part>()
        {
            new Part()
            {
                Name = "CMMG 300 Blackout Upper", Cost = -9.00
            },
            new Part()
            {
                Name = "CMMG 300 Blackout Upper mkII", Cost = 1200
            },
            new Part()
            {
                Name = "DDM4 V7", Cost = 1300
            },
            new Part()
            {
                Name = "DDM4 V7 Pro", Cost = 1500
            },
        };
    }
}