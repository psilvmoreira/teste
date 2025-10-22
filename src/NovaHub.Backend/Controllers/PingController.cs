using Microsoft.AspNetCore.Mvc;

namespace NovaHub.Backend.Controllers;

[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class PingController : BaseController
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult V1() => Ok(new { version = "v1" });

    // GET /api/v2/ping/plus
    [HttpGet("plus")]
    [MapToApiVersion("2.0")]
    public IActionResult V2() => Ok(new { version = "v2", extra = true });
}