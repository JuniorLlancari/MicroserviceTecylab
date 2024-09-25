using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;

namespace Usuarios.Application.Roles.GetRoles;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<RolesResponse>>
{
    private readonly IRolRepository _rolRepository;

    public GetRolesQueryHandler(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<List<RolesResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var result =  await _rolRepository.ListAsync(cancellationToken);

        var response = result!.Select(rol => new RolesResponse(
            rol.Id,
            rol.NombreRol!.Value,
            rol.Descripcion!.Value
        )).ToList(); 

        return response;
    }
}