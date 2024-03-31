using CodeMechanic.Neo4j;
using Neo4j.Driver;
using nugsnet6.Models;

namespace nugsnet6.Services;

public class BuilderService : IBuilderService
{
    private readonly IDriver driver;

    public BuilderService(IDriver neodriver)
    {
        driver = neodriver;
    }

    public async Task<int> StartBuild()
    {
        string query = """
        
        """;

        return 0;
    }

    public async Task<int> SeedPartTypes(int limit = 100)
    {
        var all_types = LowerPartType.GetAll<LowerPartType>();
        string query = """
            MERGE ...
        """;
        // TODO: run query.

        return 0;
    }

    public async Task<List<Build>> GetAll(
        int limit = 1000
        , Func<IRecord, Build> mapper = null
    )
    {
        string query = $"match (b:Build) return b limit {limit}";
        var parameters = new object() { };
        await using var session = driver.AsyncSession();

        try
        {
            var results = await session.ExecuteReadAsync(async tx =>
            {
                var result = await tx.RunAsync(query, parameters);
                return await result.ToListAsync<Build>(mapper);
            });

            return results;
        }

        // Capture any errors along with the query and data for traceability
        catch (Neo4jException ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("query :>> " + query);
            throw;
        }
        finally
        {
            session.CloseAsync();
        }
    }

    public Task<List<Build>> Search(Build search)
    {
        throw new NotImplementedException();
    }

    public Task<Build> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> Create(params Build[] model)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, Build model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCount()
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> FindTables()
    {
        throw new NotImplementedException();
    }

    private async Task<IList<T>> BulkCreateNodes<T>(
        string query = ""
        , object parameters = null
        , Func<IRecord, T> mapper = null
    )
        where T : class, new()
    {
        if (parameters == null) throw new ArgumentNullException(nameof(parameters));

        await using var session = driver.AsyncSession();

        try
        {
            var results = await session.ExecuteWriteAsync(async tx =>
            {
                // var _ = await tx.RunAsync(batch_command, null);
                var result = await tx.RunAsync(query, parameters);
                return await result.ToListAsync<T>(record => record.MapTo<T>());
            });

            return results;
        }

        // Capture any errors along with the query and data for traceability
        catch (Neo4jException ex)
        {
            Console.WriteLine($"{query} - {ex}");
            throw;
        }
        finally
        {
            session.CloseAsync();
        }
    }
}

//
// public class RifleBuilder
// {
//     public static RifleBuilder Start() => new RifleBuilder();
//
//     public BuildDto Complete() => MyBuild;
//
//     public RifleBuilder AddLowerParts(params Part[] parts)
//     {
//         var lower_types = new string[]
//         {
//             "Lower", "Lower Receiver", "LPK", "Lower Parts Kit", "Grip", "Trigger", "Stock", "Buttstock", "Buffer",
//             "Buffer Spring", "Castle Nut", "Takedown Pin", "Pivot Pin", "Hammer Pin", "Buffer Tube"
//         };
//
//         var valid_parts = parts.Where(part => lower_types.Contains(part.Type));
//
//         return this;
//     }
//
//     public RifleBuilder AddUpperParts(params Part[] parts)
//     {
//         var upper_types = new string[]
//         {
//             "Barrel", "Gas Tube", "Barrel Nut", "Handguard", "Foregrip", "Upper", "Upper Receiver", "BCG",
//             "Bolt Carrier Group", "Charging Handle", "Rail"
//         };
//
//         var valid_parts = parts.Where(part => upper_types.Contains(part.Type));
//
//         return this;
//     }
//
//     public RifleBuilder AddAccessories(params Part[] parts)
//     {
//         var accessories = new string[] { "Scope" };
//         var valid_parts = parts.Where(part => accessories.Contains(part.Type));
//
//         return this;
//     }
//
//     protected BuildDto MyBuild { get; set; } = new();
//
//     protected List<Part> MyParts { get; set; } = new();
// }
//
// public sealed class BuildDto
// {
//     private Build dbBuild;
//     private List<Part> dbParts;
// }