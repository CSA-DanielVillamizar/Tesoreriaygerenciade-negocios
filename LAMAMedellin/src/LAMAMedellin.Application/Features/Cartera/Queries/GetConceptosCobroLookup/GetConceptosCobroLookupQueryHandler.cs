using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetConceptosCobroLookup;

public sealed class GetConceptosCobroLookupQueryHandler(
    IConceptoCobroRepository conceptoCobroRepository)
    : IRequestHandler<GetConceptosCobroLookupQuery, List<ConceptoCobroLookupDto>>
{
    public async Task<List<ConceptoCobroLookupDto>> Handle(
        GetConceptosCobroLookupQuery request,
        CancellationToken cancellationToken)
    {
        var conceptos = await conceptoCobroRepository.GetAllAsync(cancellationToken);

        return conceptos
            .OrderBy(c => c.Nombre)
            .Select(c => new ConceptoCobroLookupDto(c.Id, c.Nombre))
            .ToList();
    }
}
