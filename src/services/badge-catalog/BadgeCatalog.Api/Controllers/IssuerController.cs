using BadgeCatalog.Ports.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BadgeCatalog.Api.Controllers;
    [ApiController]
    [Route("api/issuer")]
public class IssuerController : ControllerBase
{
    private readonly IIssuerProvider _issuerProvider;
    public IssuerController(IIssuerProvider issuerProvider)
    {
        _issuerProvider = issuerProvider;
    }
    [HttpGet]
    public  IActionResult GetIssuer()
    {
        var issuer =  _issuerProvider.GetIssuer();
        return Ok(issuer);
    }
}