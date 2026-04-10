using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetConceptosCobroLookup;

public sealed record GetConceptosCobroLookupQuery : IRequest<List<ConceptoCobroLookupDto>>;
