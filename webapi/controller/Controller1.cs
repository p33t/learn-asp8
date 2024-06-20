using Microsoft.AspNetCore.Mvc;

namespace webapi.controller;

[Route("api/[controller]")]
[ApiController]
public class Controller1 : ControllerBase
{
    [HttpGet("hello")]
    public Task<ActionResult<string>> Hello()
    {
        return Task.FromResult<ActionResult<string>>(Ok("World"));
    }
}