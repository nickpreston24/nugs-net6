using AirtableApiClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using nugsnet6;

public interface IAirtableRepo {
       Task<List<T>> ListRecords<T>(AirtableSearch search);
}