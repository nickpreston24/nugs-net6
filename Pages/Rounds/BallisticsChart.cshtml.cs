using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

// using Chart.Js;

namespace nugsnet6.Pages.Rounds
{
    public class BallisticsChartModel : PageModel
    {
        private static decimal _cached_value = 999.0m;

        public decimal CachedValue => _cached_value;

        public void OnGet()
        {
            "hello from ballistics chart".Dump();

            // Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            // ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            // {
            //     NullValueHandling = NullValueHandling.Ignore,
            // });
        }
    }
}
