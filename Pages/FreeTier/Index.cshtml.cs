using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace nugsnet6.Pages.FreeTier;

public class IndexModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;

    public IndexModel(IEmbeddedResourceQuery embeddedResourceQuery)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
    }


}