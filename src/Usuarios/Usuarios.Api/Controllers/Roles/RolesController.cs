using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Roles.GetRoles;

namespace Usuarios.Api.Controllers.Roles;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly ISender _sender;

    public RolesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerRoles(
        CancellationToken cancellationToken
    )
    {
        var query = new GetRolesQuery();
        var resultado = await _sender.Send(query,cancellationToken);
        return resultado.IsSuccess ? Ok(resultado) : BadRequest();
    }

}