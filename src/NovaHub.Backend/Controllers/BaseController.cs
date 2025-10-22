using Microsoft.AspNetCore.Mvc;

namespace NovaHub.Backend.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/")]
public class BaseController : ControllerBase { }