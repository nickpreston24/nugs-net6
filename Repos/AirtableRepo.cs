using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using AirtableApiClient;
using nugsnet6;
using CodeMechanic.Extensions;

public class AirtableRepo : IAirtableRepo {

        protected string baseId = string.Empty;
        protected string appkey = string.Empty;
        
        public AirtableRepo(string baseId = "", string appkey = "") {
            this.baseId = baseId;
            this.appkey = appkey;

            // this.appkey.Dump("api key");
            // this.baseId.Dump("base id");
        }

        public async Task<List<T>> ListRecords<T> (
            AirtableSearch search            
        ) {
            string errorMessage = string.Empty;
            var results = new List<T>();
            var records = new List<AirtableRecord>();

            var (
                offset,
                table_name, 
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

            ) = search;

            using (AirtableBase airtableBase = new AirtableBase(appkey, baseId))
            {
                //
                // Use 'offset' and 'pageSize' to specify the records that you want
                // to retrieve.
                // Only use a 'do while' loop if you want to get multiple pages
                // of records.
                //
        
                do
                {
                    Task<AirtableListRecordsResponse> task = airtableBase.ListRecords(
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
                    );

                    AirtableListRecordsResponse response = await task;

                    if (response.Success)
                    {
                        records.AddRange(response.Records.ToList());
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                        if (response.AirtableApiError is AirtableInvalidRequestException)
                        {
                            errorMessage += "\nDetailed error message: ";
                            errorMessage += response.AirtableApiError.DetailedErrorMessage;
                        }
                        break;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                        break;
                    }
                } while (offset != null);
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Error reporting
            }
            else
            {
                // Do something with the retrieved 'records' and the 'offset'
                // for the next page of the record list.
            } 

            // return records;


            foreach(var record in records ?? Enumerable.Empty<AirtableRecord>()) {
                // out string id,
                // out DateTime created_time,
                // out List<T> fields
                // var (id, created_time, fields) = record;
                // fields.Dump("fields");

                var _fields = record.Fields.Select(x=>(T)x.Value).ToList().Dump("fields");
                // results.Add(fields);
            }

            return results;

        }

    }
