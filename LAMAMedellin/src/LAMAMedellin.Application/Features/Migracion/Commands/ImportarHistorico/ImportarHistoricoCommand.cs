using MediatR;

namespace LAMAMedellin.Application.Features.Migracion.Commands.ImportarHistorico;

/// <summary>
/// Comando para importar datos históricos desde CSV (Enero 2025 - Febrero 2026).
/// Lee el archivo docs/Historico.csv y genera comprobantes contables automáticamente.
/// </summary>
public sealed record ImportarHistoricoCommand : IRequest<ImportarHistoricoResult>;

public sealed record ImportarHistoricoResult(
    int ComprobantesCreados,
    int LineasProcesadas,
    List<string> Advertencias);
