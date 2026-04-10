using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetMiembrosLookup;

public sealed class GetMiembrosLookupQueryHandler(
    IMiembroRepository miembroRepository)
    : IRequestHandler<GetMiembrosLookupQuery, List<MiembroLookupDto>>
{
    public async Task<List<MiembroLookupDto>> Handle(
        GetMiembrosLookupQuery request,
        CancellationToken cancellationToken)
    {
        var miembros = await miembroRepository.GetAllAsync(cancellationToken);

        return miembros
            .OrderBy(m => m.Nombres)
            .ThenBy(m => m.Apellidos)
            .Select(m => new MiembroLookupDto(
                m.Id,
                $"{m.Nombres} {m.Apellidos}".Trim()))
            .ToList();
    }
}
