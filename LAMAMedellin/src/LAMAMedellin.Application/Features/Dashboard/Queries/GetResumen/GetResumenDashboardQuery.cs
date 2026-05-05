using MediatR;

namespace LAMAMedellin.Application.Features.Dashboard.Queries.GetResumen;

public sealed record GetResumenDashboardQuery : IRequest<DashboardResumenDto>;
