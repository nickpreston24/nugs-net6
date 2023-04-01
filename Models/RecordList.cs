using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CodeMechanic.Extensions;

namespace CodeMechanic.Airtable;

public class RecordList<T>
{
    private string json;
    private List<T> records = new List<T>();

    public RecordList(string json = """
        {
            records: [
                {"id": "rec00c47kATcdB0Ub","createdTime": "2022-11-27T04:39:23.000Z",
                fields: {}
                ]}
            ]
        }
        """) 
    {
        this.json = json;
        // this.records = JsonConvert.DeserializeObject<List<T>>(json);

        /// https://www.newtonsoft.com/json/help/html/SerializingJSONFragments.htm

        JObject search = JObject.Parse(json);
        // search.Dump("Jobject");

        // get JSON result objects into a list
        IList<JToken> results = search["records"]
            .Children()/*.Dump("children")*/["fields"]
            /*.Dump("fields children")*/
            .ToList();
  
        // serialize JSON results into .NET objects
        IList<T> records = new List<T>();
        foreach (JToken result in results)
        {
            // JToken.ToObject is a helper method that uses JsonSerializer internally
            T instance = result.ToObject<T>();
            this.records.Add(instance);
        }
        // this.records.Dump("extracted records");
    }

    public void Deconstruct(
        out string raw_json,
        out List<T> records
    )
    {
        raw_json = this.json;
        records = this.records/*.Dump("returned records")*/;
    }
}