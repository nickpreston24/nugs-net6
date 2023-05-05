using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CodeMechanic.Extensions;


using CodeMechanic.Embeds;
namespace nugsnet6.Pages.Contribute;

public class IndexModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;

    public IndexModel(IEmbeddedResourceQuery embeddedResourceQuery)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
    }

    
}