using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using AirtableApiClient;
using CodeMechanic.Extensions;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using nugsnet6;
using nugsnet6.Models;
using nugsnet6.Airtable;

public class AirtableRepo : IAirtableRepo {

        protected string base_id = string.Empty;
        // protected string api_key = string.Empty;
        protected string personal_access_token = string.Empty;
        private readonly HttpClient http_client;
        
        public AirtableRepo(HttpClient client, string base_id = "", string personal_access_token = "") {
            this.base_id = base_id;
            this.personal_access_token = personal_access_token;
            // this.api_key = api_key;  // DEPRECATED
 
            this.personal_access_token.Dump("personal_access_token");
            this.base_id.Dump("base id");

            this.http_client = client;
            this.http_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", personal_access_token);
        }

        public async Task<List<T>> SearchRecords<T> (AirtableSearch search)
        { 
             var (
                table_name, 
                offset,
                fields, 
                filterByFormula, 
                maxRecords, 
                pageSize, 
                sort, 
                view,
                cellFormat,
                timeZone,
                userLocale,
                returnFieldsByFieldId
            ) = search.Dump("sent search");

            if(string.IsNullOrEmpty(table_name))
                table_name = nameof(Loadout) + "s";

            var response = await http_client.GetAsync($"{base_id}/{table_name}?maxRecords={maxRecords}");
            // response.Dump("httpclient response");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            // Console.WriteLine("Here's the raw JSON:");
            // Console.WriteLine(json);

            var list = new RecordList<T>(json);
            var (_, records) = new RecordList<T>(json);
            return records;
        }

        // public async Task<List<T>> GetRecordsAsync<T>() {
        //     //var stock = await httpClient.GetFromJsonAsync<Stock>($"https://localhost:12345/stocks/{symbol}");

        //     // var uri = $"https://api.airtable.com/v0/{base_id}" + "/Parts";
        //     // var response = await http_client.GetFromJsonAsync<List<T>>($"{base_id}/Parts?maxRecords=2");

        //     var response = await http_client.GetAsync($"{base_id}/Parts?maxRecords={search.maxRecords}");
        //     response.Dump("httpclient response");
        //     response.EnsureSuccessStatusCode();

        //     var json = await response.Content.ReadAsStringAsync();
        //     Console.WriteLine("Here's the raw JSON:");
        //     Console.WriteLine(json);

        //     var list = new RecordList<T>(json);
        //     // var records = list.records.Dump("outer records");
        //     var (_, records) = new RecordList<T>(json);
        //     // var records =  new List<T>();
        //     return records;
        // }

        // public async Task<List<Part>> GetAsync() {
        //     var client = new RestClient($"https://api.airtable.com/v0/{base_id}");
        //     var request = new RestRequest("Parts");
        //         request.AddHeader("authorization", "bearer "+ this.personal_access_token);
        //     // var results = await client.GetAsync<List<Part>>(request).Data;
        //     var response = await client.GetJsonAsync<List<Part>>("endpoint?foo=bar");

        //     response.Dump("Raw");
        //     return response;
        // }


        // public async Task<List<T>> ListRecords<T> (
        //     AirtableSearch search            
        // ) {
        //     string errorMessage = string.Empty;
        //     var results = new List<T>();
        //     var records = new List<AirtableRecord>();

        //     var (
        //         offset,
        //         table_name, 
        //         fields, 
        //         filterByFormula, 
        //         maxRecords, 
        //         pageSize, 
        //         sort, 
        //         view,
        //         cellFormat,
        //         timeZone,
        //         userLocale,
        //         returnFieldsByFieldId

        //     ) = search;

        //     using (AirtableBase airtableBase = new AirtableBase(api_key, base_id))
        //     {
        //         //
        //         // Use 'offset' and 'pageSize' to specify the records that you want
        //         // to retrieve.
        //         // Only use a 'do while' loop if you want to get multiple pages
        //         // of records.
        //         //
        
        //         do
        //         {
        //             Task<AirtableListRecordsResponse> task = airtableBase.ListRecords(
        //                 table_name.Dump("tablename in"), 
        //                 offset, 
        //                 fields.Dump("fields in"), 
        //                 filterByFormula, 
        //                 maxRecords, 
        //                 pageSize, 
        //                 sort, 
        //                 view,
        //                 cellFormat,
        //                 timeZone,
        //                 userLocale,
        //                 returnFieldsByFieldId
        //             );

        //             AirtableListRecordsResponse response = await task;

        //             response.Dump("list records response");

        //             if (response.Success.Dump("success?"))
        //             {
        //                 records.AddRange(response.Records.ToList());
        //                 offset = response.Offset;
        //             }
        //             else if (response.AirtableApiError is AirtableApiException)
        //             {
        //                 errorMessage = response.AirtableApiError.ErrorMessage;
        //                 errorMessage.Dump("error");
        //                 if (response.AirtableApiError is AirtableInvalidRequestException)
        //                 {
        //                     errorMessage += "\nDetailed error message: ";
        //                     errorMessage += response.AirtableApiError.DetailedErrorMessage;
        //                 }
        //                 break;
        //             }
        //             else
        //             {
        //                 errorMessage = "Unknown error";
        //                 break;
        //             }
        //         } while (offset != null);
        //     }

        //     if (!string.IsNullOrEmpty(errorMessage))
        //     {
        //         // Error reporting
        //     }
        //     else
        //     {
        //         // Do something with the retrieved 'records' and the 'offset'
        //         // for the next page of the record list.
        //     } 

        //     // return records;


        //     foreach(var record in records.Dump("Records from table") ?? Enumerable.Empty<AirtableRecord>()) {
        //         // out string id,
        //         // out DateTime created_time,
        //         // out List<T> fields
        //         // var (id, created_time, fields) = record;
        //         // fields.Dump("fields");

        //         var _fields = record.Fields.Select(x=>(T)x.Value).ToList().Dump("fields");
        //         // results.Add(fields);
        //     }

        //     return results;

        // }

    }
