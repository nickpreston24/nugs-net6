/*
   
    CHECKLIST:
    0. Fix the issue where the target div is rendering above the table
    1. Create a HtmxTableModel that spits out a fully generated and customizable table.
    2. Use Func<T, string> to pass the row generator into <tbody>
    3. Bonus - add support for DaisyUI class variants ()
    4. Bonus - add sorting, filtering, paging and search (done)
    5. Bonus - Toggle the search, filters, etc.
   
*/
using CodeMechanic.Extensions;
using System.Text;
namespace nugsnet6.Models;

public class HTMXTable<T>
{
    public IEnumerable<T> Rows { get; set; } = new List<T>();
    public Func<T, string> row_generator { get; set; }// = (_) => string.Empty;
    
    public Dictionary<int, IList<Func<T,T>>> Sorts { get; set; } = new Dictionary<int, IList<Func<T,T>>>(); //TODO: use the If<T>(...) method you found

    public Dictionary<int, IList<Func<T,T>>> Filters { get; set; } = new  Dictionary<int, IList<Func<T,T>>>(); //TODO: implement that awesome Enum code you found which makes strings to Enum support easy, OR install a Nuget you found that does the same.


    public Func<IEnumerable<T>, IEnumerable<T>> Pagination { get; set; } // TODO: let the user set the pagination function, e.g. (offset, pagesize, max, step, etc).  Easily done, with one Func, vs a bunch of props.  Possible to use the NSpecification library to easily implement this w/o too much hard work.

    public string ToHTML() {
        string rows_html = new StringBuilder().AppendEach(Rows, row_generator).ToString();
        return $"""
        <table class='table table-compact'>
            {rows_html}
        </table>
        """;
    }
}