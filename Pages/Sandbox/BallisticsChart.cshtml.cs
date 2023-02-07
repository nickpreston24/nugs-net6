using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Chart.Js;
using nugsnet6.Models;

namespace nugsnet6.Pages.Sandbox
{
    public class BallisticsChartModel : PageModel
    {
        private static decimal _cached_value = 999.0m;

        public decimal CachedValue => _cached_value;

        public void OnGet()
        {

            // Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            // ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            // {
            //     NullValueHandling = NullValueHandling.Ignore,
            // });
        }        
    }
}
