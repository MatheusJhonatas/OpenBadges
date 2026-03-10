using BadgeCatalog.Adapters.Security;
using BadgeCatalog.Ports.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BadgeCatalog.Api.Controllers;

[ApiController]
[Route("api/keys")]
public class KeysController : ControllerBase
{
    private readonly IJwkProvider _provider;

    public KeysController(IJwkProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("current")]
    public IActionResult GetCurrentKey()
    {
        var key = _provider.GetCurrent();
        return Ok(key);
    }
}