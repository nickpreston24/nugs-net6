using Bogus;
using Bogus.DataSets;
using CodeMechanic.Diagnostics;

namespace CodeMechanic.RazorHAT.Services;

public interface IFakerService
{
    // string[] GetAllRoutes();
}

public class FakerService : IFakerService
{
    private readonly bool dev_mode;
    // private readonly IEnumerable<string> razor_page_routes;
    // private readonly 

    public FakerService(bool dev_mode = false)
    {
        /**
         * Lucky Gunner
84	224 Valkyrie - 75 Grain TMJ - Federal American Eagle - 200 Rounds - Brass	American Eagle	.224 Valkyrie	75	2m16s	-	brass		$135.00	200	67.5¢
7
         */
        // razor_page_routes = GetAllRoutes();
        this.dev_mode = dev_mode;
    }

    public class User
    {
        public User(int i, string replace)
        {
        }

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

    public User GetFakeUser()
    {
        //Set the randomizer seed if you wish to generate repeatable data sets.
        Randomizer.Seed = new Random(8675309);

        // var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };

        var userIds = 0;
        var testUsers = new Faker<User>()
            //Optional: Call for objects that have complex initialization
            .CustomInstantiator(f => new User(userIds++, f.Random.Replace("###-##-####")))

            //Use an enum outside scope.
            .RuleFor(u => u.Gender, f => f.PickRandom<Name.Gender>())

            //Basic rules using built-in generators
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
            .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")

            //Use a method outside scope.
            // .RuleFor(u => u.CartId, f => Guid.NewGuid())
            //Compound property with context, use the first/last name properties
            .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
            //And composability of a complex collection.
            // .RuleFor(u => u.Orders, f => testOrders.Generate(3).ToList())
            //Optional: After all rules are applied finish with the following action
            .FinishWith((f, u) => { Console.WriteLine("User Created! Id={0}", u.Id); });

        var user = testUsers.Generate();
        Console.WriteLine(user.Dump());

        return user;
    }
}