using System.Collections.Specialized;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Application.Queries.GetAllBadges;
using Microsoft.AspNetCore.Mvc;

namespace BadgeCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BadgesController : ControllerBase
{
    private readonly CreateBadgeClassHandler _createBadgeClass;
    public BadgesController(CreateBadgeClassHandler createBadgeClass)
    {
        _createBadgeClass = createBadgeClass;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBadgeClass(CreateBadgeClassCommand command, CancellationToken cancellationToken)
    {
        var id = await _createBadgeClass.Handle(command, cancellationToken);
        return Created($"/api/badges/{id}", new { Id = id });
    }
}
