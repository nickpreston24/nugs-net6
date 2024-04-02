/* PSA: Use these properties in as many tables as possible for self-auditing tables.
  You never know when that one 0.0001% edge case happens and no one knows why it happened.  Well, now you can get closer.

  PSA:  Don't log just anything like INFO. Be very selective as to what we log. 
  This should be a helpful table, not a burden.

  Suggest logging 'INFO' as local *.txt files.  No one wants to read through happy-path INFO logs unless they have to.  The real meat is within WARN and ERROR.  Otherwise, you may have to use Splunk to filter the noise out.  It's unnecesary, if we're proactively logging things with as much useful details as possible.

  PSA: Use a special logger like Serilog or NLog to make this super easy.
  To make this stupid-easy to maintain by using Dapper or Insight.Database.  
  (Good way to avoid arthritis and premature baldness at the same time).
*/

using Microsoft.Data.SqlClient;
namespace CodeMechanic.Models;

public sealed class LogRow
{

    public int Id { get; set; } = -1;


    public string TableName { get; set; } 
    public string ServerName { get; set; } 
    public string DatabaseName { get; set; } 

    // e.g., the Stack Trace.
    public string ExceptionMessage { get; set; } 

    // The path you took to get here, e.g. "Maintenance > CEDNet Systems Settings > User/Security Maintenance"
    // This lets anyone viewing this table know THE EXACT business path another person took to create the bug.  Useful for everyone and helps de-tribalize knowledge.  All in one teensy little property! :P
    public string Breadcrumb { get; set; } 


    // This should be JSON and it should hold only the values changed when calling UPDATEs or even DELETEs.
    // E.g.:
    // ``` json
    // 9ciCustomers: [{ 
    //    id: 12345,
    //    fields: [{key: "Name", oldvalue: "Nicholas Preston", newvalue: "Nick Preston, esq. the 3rd, eccelsior"}] }},
    // ...
    // }]
    // ```
    // Something like that. Whatever's easiest to parse out after Deserializing the JSON string.
    // This also makes it really easy to ROLLBACK items which had an error mid-transaction
    public string Diff { get; set; } 

    // These should be ToString()'d into a logging table as JSON or a custom ToString() method.  
    // Storing these allows us to easily figure out what we sent to the database, without necessarily having to create an audit table for every domain table in CED.
    public SqlParameter[] RawValues { get; set; } = Array.Empty<SqlParameter>();
    
    

    // if it exists, log it.
    public string AssemblyName { get; set; } 

    // if it exists, log it.
    public string Namespace { get; set; } 


    /* Essential Auditing properties for tables */
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }
    // Person or program name
    public string LastModifiedBy { get; set; } 
    // Person or program name
    public string CreatedBy { get; set; } 


    /* Optional Meta Properties
    These are highly flexible Rich Text (255->Max) fields, intended to catch varying bits of information that can be resolved in the App or simply viewed.
     */
    public string IssueSource { get; set; }= string.Empty;// a Task, Story, Epic, or anything that is tracked.

    public string Commit { get; set; }= string.Empty;// can be a URL or a Git/TFS commit #.

}
