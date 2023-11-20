using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CodeMechanic.Diagnostics;
using CodeMechanic.RazorHAT;
using CodeMechanic.Embeds;
using CodeMechanic.Extensions;
using Insight.Database;
using Neo4j.Driver;
using Npgsql;
using nugs_seeder.Controllers;
using nugsnet6.Models;

namespace nugsnet6.Pages.Sandbox;

[BindProperties]
public class IndexModel : HighSpeedPageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;
    private readonly string postgresql_connectionstring;
    private bool dev_mode = true;

    public AmmoseekRow Insert { get; set; } = new AmmoseekRow();
    public List<AmmoseekRow> AmmoseekRows { get; set; } = new List<AmmoseekRow>();

    [BindProperty] public string Retailer { get; set; } = string.Empty;
    [BindProperty] public string Message { get; set; } = string.Empty;


    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver)
        : base(embeddedResourceQuery, driver)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;

        string host = Environment.GetEnvironmentVariable("PGHOST");
        string username = Environment.GetEnvironmentVariable("PGUSER");
        string password = Environment.GetEnvironmentVariable("PGPASSWORD");
        string database = Environment.GetEnvironmentVariable("PGDATABASE");
        string port = Environment.GetEnvironmentVariable("PGPORT");

        postgresql_connectionstring =
            $"Host={host};Port={port};Username={username};Password={password};Database={database}";
    }


    public void OnGet()
    {
    }

    public IActionResult OnPostModal()
    {
        Message.Dump("message submitted");
        return Partial("Success", this);
    }

    public async Task<IActionResult> OnGetRecommendedRifles()
    {
        var failure = Content(
            $"<div class='alert alert-error'><p class='text-xl text-warning text-sh'>An Error Occurred...  But fret not! Our team of intelligent lab mice are on the job!</p></div>");

        string query = "...";

        // Magically infers that the current method name is referring to 'RecommendedNugs.cypher'
        string resource = new StackTrace().GetCurrentResourcePath().Dump("resource path");
        if (embeddedResourceQuery == null)
            return failure;

        // throw new Exception("d'oh!");
        // Reads from file system...
        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);
        if (stream == null)
            Console.WriteLine("Oh no!  I'm deaf!");

        // Reads the any file I tell it to as a query.
        query = await stream.ReadAllLinesFromStreamAsync();


        // run query

        // This can also be a template
        return Content(
            $"<div class='alert alert-primary'><p class='text-xl text-secondary text-sh'>{query}</p></div>");

        // return JsonResult(records); // mustache

        // return Content(
        //     $"""
        //     <div class='alert alert-primary'>
        //         <p class='text-xl text-secondary text-sh'>{query}</p>
        //     </div>
        //     """);
    }

    public async Task<IActionResult> OnGetAmmoseekPrices()
    {
        // if (dev_mode) Console.WriteLine("Checking ammo prices...");
        try
        {
            await using var connection = new NpgsqlConnection(postgresql_connectionstring);
            await connection.OpenAsync(); // needed?
            var results = connection.QuerySql<AmmoseekRow>("select * from ammoseek_prices");

            // if (dev_mode) results.Dump("ammoseek rows");
            return Partial("_AmmoseekTable", results);
        }
        catch (Exception ex)
        {
            return Partial("_Alert",
                new AlertModel() { Error = ex, Message = "You screwed up." });
        }
    }


    public async Task<IActionResult> OnPostBulkUploadAmmoseekRows()
    {
        return Content(" bulk upload submitted <3!");
    }

    /// <summary>
    /// Insert a new Ammoseek Row via form
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostInsertAmmoseekRow()
    {
        if (dev_mode) Console.WriteLine("Inserting ammo prices...");
        Insert.Dump("inserted row");
        // return Content("Ping!");

        string query = """
            
        INSERT INTO ammoseek_prices(retailer,
                            description,
                            brand,
                            caliber,
                            grains,
                            last_update,
                            limits,
                            casing,
                            is_new,
                            price,
                            rounds,
                            price_per_round,
                            shipping_rating)

        VALUES ( 'Botachzzz'
               , 'Fiocchi Shooting Dynamics .300 Blackout 150 Grain FMJBT Ammunition 50rds 762345000000zzz'
               , 'Fiocchizzz'
               , '.300 AAC Blackoutzzz'
               , '150'
               , '-'
               , '-'
               , 'brass'
               , true
               , '$66.95'
               , '50'
               , '$1.34'
               , '6'),
               ( 'LeadFeather Guns & Ammo'
               , 'Fiocchi 150gr FMJBT 300BLK 762344711935'
               , 'Fiocchi'
               , '.300 AAC Blackout'
               , '150'
               , '2m9s'
               , '-'
               , 'brass'
               , true
               , '$149.99'
               , '100'
               , '$1.50'
               , '5');
        """;

        try
        {
            await using var connection = new NpgsqlConnection(postgresql_connectionstring);
            await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("retailer", "Ziggy Stardust zzz");

                int rows_affected = await cmd.ExecuteNonQueryAsync();
                if (dev_mode) Console.WriteLine("Rows affected :>> " + rows_affected);
            }

            return Partial("_AmmoseekTable", AmmoseekRows);
        }
        catch (Exception ex)
        {
            return Partial("_Alert", new AlertModel() { Error = ex, Message = "You screwed up." });
        }
    }


    public async Task<IActionResult> OnGetSamplePostgresSchema()
    {
        var database = new SqlConnectionStringBuilder(postgresql_connectionstring);
        // NOTE: when returning cursors, you need to open the query in a transaction (eek)

        using (var connection = database.OpenWithTransaction())
        {
            connection.ExecuteSql("CREATE TABLE PostgreSQLTestTable (p int)");
            connection.ExecuteSql(@"
					CREATE OR REPLACE FUNCTION PostgreSQLTestProc (i int) 
					RETURNS SETOF refcursor
					AS $$
					DECLARE
						rs refcursor;
					BEGIN 
						INSERT INTO PostgreSQLTestTable VALUES (@i);
						OPEN rs FOR SELECT * FROM PostgreSQLTestTable;
						RETURN NEXT rs;
					END;
					$$ LANGUAGE plpgsql;");

            var results = connection.Query<int>("PostgreSQLTestProc", new { i = 5 });
            // Assert.AreEqual(1, result.Count);
            // Assert.AreEqual(5, result[0]);

            return Partial("_PostgresTestProcResult", results);
        }
    }
}


// var input = new TestData();
// var testData = new TestData() { X = 1, Z = 2 };
// var user = new User() { Id = 1, JsonData = input, Ebay = "" };

// string query = """
//                SELECT 'Ebay' FROM Users
//                """;
// round-trip the object into JSON
// connection.ExecuteSql("INSERT INTO Users (Id, JsonData) VALUES (@Id, @JsonData)", user);