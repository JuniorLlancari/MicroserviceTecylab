using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Roles.GetRoles;

public sealed record GetRolesQuery () : IQuery<List<RolesResponse>>;
