

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using AirtableApiClient;

using nugsnet6;

public interface IAirtableRepo {
        Task<List<AirtableRecord>> GetRecords(string table_name, string offset);
    }

public abstract class AirtableRepo : IAirtableRepo {

        protected string baseId = string.Empty;
        protected string appkey = string.Empty;
        
        public AirtableRepo(string baseId, string appkey) {
            this.baseId = baseId;
            this.appkey = appkey;
        }
        
        public async Task<List<AirtableRecord>> GetRecords (
            string table_name
            , string offset = "0"
        ) {
            string errorMessage = string.Empty;
            var records = new List<AirtableRecord>();

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
                        offset 
                        //, fields, 
                        // filterByFormula, 
                        // maxRecords, 
                        // pageSize, 
                        // sort, 
                        // view,
                        // cellFormat
                        // timeZone,
                        // userLocale,
                        // returnFieldsByFieldId
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

            return records;

        }

    }
