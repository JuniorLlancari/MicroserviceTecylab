using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Jwt;
using Usuarios.Application.Usuarios.CrearUsuario;
using Usuarios.Application.Usuarios.GetUsuario;

namespace Usuarios.Api.Controllers.Usuarios;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly ISender _sender;

    private readonly IAutorizationService _autorizationService;

    public UsuarioController(ISender sender, IAutorizationService autorizationService)
    {
        _sender = sender;
        _autorizationService = autorizationService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerUsuario(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetUsuarioQuery(id);
        var resultado = await _sender.Send(query,cancellationToken);
        return resultado.IsSuccess ? Ok(resultado) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CrearUsuario(
        CrearUsuarioRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new CrearUsuarioCommand
        (
            request.Password,
            request.Rol,
            request.Nombres,
            request.ApellidoPaterno,
            request.ApellidoMaterno,
            request.FechaNacimiento,
            request.Pais,
            request.Departamento,
            request.Provincia,
            request.Ciudad,
            request.Distrito,
            request.Calle,
            request.CorreoElectronico
        );

        var resultado = await _sender.Send(command,cancellationToken);

        if(resultado.IsSuccess){
            return CreatedAtAction(nameof(ObtenerUsuario), new { id = resultado.Value } , resultado.Value );
        }
        return BadRequest(resultado.Error);

    }

    [HttpPost]
    [Route("Autenticar")]
    public async Task<IActionResult> Autenticar(
        AutorizacionRequest autorizacion,
        CancellationToken cancellationToken
    )
    {
        var resultado = await _autorizationService.DevolverToken(autorizacion,cancellationToken);
        if (resultado == null)
        {
            return Unauthorized();
        }
        return Ok(resultado);
    }
}