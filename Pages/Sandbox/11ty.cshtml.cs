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
  
namespace nugsnet6.Pages.Sandbox;

public class EleventyModel: PageModel
{

    public EleventyModel() 
    {
 
    }

    public async Task<IActionResult> OnGetStuff() {

        return Content("<b>stuff aqcuired</b>");
    }
}