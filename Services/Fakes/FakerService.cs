using System.Globalization;
using Bogus;
using Bogus.DataSets;
using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using Newtonsoft.Json;
using nugsnet6.Models;

namespace CodeMechanic.RazorHAT.Services;

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

    public BogusUser GetFakeUser()
    {
        //Set the randomizer seed if you wish to generate repeatable data sets.
        Randomizer.Seed = new Random(8675309);

        // var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };

        var userIds = 0;
        var testUsers = new Faker<BogusUser>()
            //Optional: Call for objects that have complex initialization
            .CustomInstantiator(f => new BogusUser(userIds++, f.Random.Replace("###-##-####")))
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
            .FinishWith(
                (f, u) =>
                {
                    Console.WriteLine("User Created! Id={0}", u.Id);
                }
            );

        var user = testUsers.Generate();
        Console.WriteLine(user.Dump());

        return user;
    }

    public List<AmmoseekRow> GetFakeAmmoPrices(int limit = 1000)
    {
        var retailers = new[]
        {
            "Gorilla",
            "Hornady",
            "American Federal",
            "Sellier & Bellot",
            "Blastechzzz",
        };

        var calibers = new[]
        {
            "300 AAC Blackout",
            "5.56 NATO",
            "7.62x39",
            ".308 Winchester",
            "6.5 Creedmoor",
            "6mm ARC",
            ".224 Valkyrie",
        };
        var grains = new[]
        {
            "110",
            "120",
            "125",
            "140",
            "160",
            "150",
            "200",
            "180",
            "210",
            "250",
            "300",
        };

        var rows = Enumerable
            .Range(1, limit)
            .Select(index =>
                new AmmoseekRow()
                {
                    retailer = retailers.TakeFirstRandom(),
                    price_per_round = (Bogus.Randomizer.Seed.NextDouble() * 7.50).ToString(),
                    created_by = "Nicholas Preston",
                    grains = grains.TakeFirstRandom(),
                }.With(x => x.brand = x.retailer)
            )
            .ToList();

        rows.Take(1).Dump("sample");
        return rows;
    }

    public List<Part> ImportPartsFromFile()
    {
        string filepath = Path.Combine(
            Environment.CurrentDirectory,
            "Experimental",
            "parts-from-csv.json"
        );

        Console.WriteLine("parts json :>> " + filepath);

        string json = File.ReadAllText(filepath);

        var parts = JsonConvert.DeserializeObject<List<Part>>(json) ?? new List<Part>();
        if (dev_mode)
            parts.TakeFirstRandom().Dump("first part");

        return parts;

        // string text = """
        //             Name,Cost,Attachments,URL,Calibers,WeightInOz,Demo,Type,Kind,Notes,Combo,Combo Cost,Builds,Requests,Donations,Created By,Created,Last Modified By,Last Modified,ProductCode,Id,Roles,Delimiter
        // 80% Arms Billet Lower (Blemished),$79.00,image.png (https://v5.airtableusercontent.com/v2/23/23/1700503200000/u2po4Mvfs5IyQ_ZW_yiuGQ/wIX652rM_yhv6_A3N1yuNTIbtvknqaewNCLPQjEmC7oCzS3Z1vHqVNxdvj7VKSLsfwjdvu81S7zi2eP1AntBHLUPojB5krxOAlLPmTS8a6gh0RD3Ii_9XB6k5ofHsfxcb59U0keWAX77ri_04_RCvA/7ueSd3j93G78pKeXaJuMSs7NUYDg_aLvQ73CieZrlJg),https://www.80percentarms.com/products/type-iii-hard-anodized-billet-ar-15-80-lower-receiver-blemished/,Any,7.50,,Lower,,"Reccommended by Pew Pew Tactical as their #6, I believe this one could aircraft grade aluminum.",,$0.00,"Natasha,Banshee / Sirius Blackout,Spectre / Spectre Warlock ,Zev Striker,SPECTRE / Warlock (Lower Only),Cruise Cannon,The Minuteman,Nuremburg Needler (Ti),Nuremburg Needler (Budget),Sasha,Kitschy Kestrel,Budget Blaster,Economy Equine,Filly Folder,Kitschy Kestrel (DIY),Rangy RECCE,Prancing Poltergeist,Flyweight Filly (DIY),M-4 Replacement (Budget),M5/M249 Replacement (Budget),M-4 GRN Replacement (Budget),ARAK-Nid,Minuteman - SALE","11,24,31,33,27,9,47","29,45,63,91",Nicholas Preston,6/16/2021 8:09pm,Nicholas Preston,11/20/2023 8:14am,,87,,;!
        // FN Barrel (chrome lined),$300.00,,https://www.brownells.com/rifle-parts/barrel-parts/rifle-barrels/ar-15-m16-5-56x45mm-cold-hammer-forged-barrels-prod71548.aspx?avad=194130_f2255473d&aid=155690&cm_mmc=affiliate-_-Itwine-_-Avantlink-_-Custom+Link&utm_medium=affiliate&utm_source=Avantlink&utm_content=NA&utm_campaign=Itwine,5.56 NATO,,,Barrel,,"Chrome lining, full-auto rated; lasts longer",,$0.00,Sasha,,,Nicholas Preston,6/16/2021 8:09pm,Nicholas Preston,11/20/2023 8:14am,,895,,;!
        // """;
    }

    public class PriceConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(double); // Assuming it's a struct
        }

        public override object ReadJson(
            JsonReader reader,
            System.Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            if (reader.TokenType != JsonToken.Null)
            {
                if (
                    reader.TokenType == JsonToken.String
                    || reader.TokenType == JsonToken.Integer
                    || reader.TokenType == JsonToken.Float
                )
                {
                    try
                    {
                        var value = Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture);

                        return new double();
                    }
                    catch (Exception ex)
                    {
                        throw new JsonSerializationException(
                            string.Format("Error converting value '{0}' to double.", reader.Value),
                            ex
                        );
                    }
                }

                throw new JsonSerializationException(
                    string.Format(
                        "Unexpected token when parsing double. Got {0}.",
                        reader.TokenType
                    )
                );
            }

            if (objectType != typeof(double))
            {
                throw new JsonSerializationException(
                    string.Format("Cannot convert null value to double")
                );
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }
    }
}
