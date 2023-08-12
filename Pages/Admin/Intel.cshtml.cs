using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Admin;

public class Intel : PageModel
{
    private static int port = -1;
    private static string endpoint = string.Empty;

    public void OnGet()
    {
        port = 8090;
        endpoint = $"http://127.0.0.1:{port}";
    }


    /*// JavaScript SDK


// list and search for 'example' collection records
const list = await pb.collection('example').getList(1, 100, {
    filter: 'title != "" && created > "2022-08-01"',
    sort: '-created,title',
});

// or fetch a single 'example' collection record
const record = await pb.collection('example').getOne('RECORD_ID');

// delete a single 'example' collection record
await pb.collection('example').delete('RECORD_ID');

// create a new 'example' collection record
const newRecord = await pb.collection('example').create({
    title: 'Lorem ipsum dolor sit amet',
});

// subscribe to changes in any record from the 'example' collection
pb.collection('example').subscribe('*', function (e) {
    console.log(e.record);
});

// stop listening for changes in the 'example' collection
pb.collection('example').unsubscribe();

*/
}