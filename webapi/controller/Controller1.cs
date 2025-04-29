using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace webapi.controller;

/// <summary>This is a test controller </summary>
[Route("api/[controller]")]
[ApiController]
public class Controller1 : ControllerBase
{
    /// <summary>
    /// Some XML Docs (these need &lt;summary&gt; tag to land in Swagger)
    /// </summary>
    [HttpGet("hello")]
    public Task<ActionResult<string>> Hello([FromQuery] HelloParam parameter)
    {
        return Task.FromResult<ActionResult<string>>(Ok("World"));
    }

    /// These comments that don't have "summary" tag now appears because of custom processing. And it has extra markup <see cref="Controller1"/>.
    [HttpGet("getObj")]
    public Task<ActionResult<HelloParam>> GetObj()
    {
        return Task.FromResult<ActionResult<HelloParam>>(Ok(new HelloParam
        {
            name = "Macbeth"
        }));
    }
    
    // Seems to be the easiest way to have a FromQuery object with proper camelCase query args.
    // Response object automatically have camelCase names when property is PascalCase.
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class HelloParam
    {
        [Required(AllowEmptyStrings = false)]
        [DefaultValue("Bruce")]
        public string name { get; set; } = string.Empty;
    }
}