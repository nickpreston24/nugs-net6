using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CodeMechanic.Extensions;
using CodeMechanic.RazorPages;
using Neo4j.Driver;


namespace nugsnet6.Pages.FreeTier;

public class IndexModel : HighSpeedPageModel
{
    public IndexModel(IEmbeddedResourceQuery embeddedResourceQuery
    , IDriver driver) : base(embeddedResourceQuery, driver)
    {
    }
}