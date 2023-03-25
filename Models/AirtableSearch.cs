using nugsnet6;

using AirtableApiClient;

// public record AirtableSearch(

//     string table_name, 
//     string offset, 
//     List<string> fields, 
//     string filterByFormula, 
//     int maxRecords, 
//     int pageSize, 
//     List<Sort> sort, 
//     string view, 
//     string cellFormat, 
//     string timeZone, 
//     string userLocale, 
//     bool returnFieldsByFieldId 
// );

public class AirtableSearch 
{
    public string table_name  { get; set; } = string.Empty;
    public string offset  { get; set; } = string.Empty;
    public List<string> fields { get; set; } = new List<string>();
    public string filterByFormula { get; set; } = string.Empty;
    public int maxRecords { get; set; } = 20;
    public int pageSize { get; set; } = 10;
    public List<Sort> sort { get; set; } = new List<Sort>();
    public string view { get; set; } = string.Empty;
    public string cellFormat { get; set; } = string.Empty;
    public string timeZone { get; set; } = string.Empty;
    public string userLocale { get; set; } = string.Empty;
    public bool returnFieldsByFieldId { get; set; } = true;  
    // public bool includeCommentCount { get; set; } = true;

    public void Deconstruct(
        out string table_name, 
        out string offset, 
        out List<string> fields, 
        out string filterByFormula, 
        out int maxRecords, 
        out int pageSize, 
        out List<Sort> sort, 
        out string view, 
        out string cellFormat, 
        out string timeZone, 
        out string userLocale, 
        out bool returnFieldsByFieldId 
    )
    {
        table_name =  this.table_name; 
        offset =  this.offset; 
        fields =  this.fields; 
        filterByFormula =  this.filterByFormula; 
        maxRecords =  this.maxRecords; 
        pageSize =  this.pageSize; 
        sort =   this.sort; 
        view =   this.view; 
        cellFormat =   this.cellFormat; 
        timeZone =   this.timeZone; 
        userLocale =   this.userLocale; 
        returnFieldsByFieldId =  this.returnFieldsByFieldId;
    }
}

public static class AirtableExtensions {


   
}