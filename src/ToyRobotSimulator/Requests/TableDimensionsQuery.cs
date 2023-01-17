using MediatR;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Responses;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Query that returns the table dimensions.
/// </summary>
public class TableDimensionsQuery : IRequest<TableDimensionsQueryResponse> { }

public class TableDimensionsQueryHandler : IRequestHandler<TableDimensionsQuery, TableDimensionsQueryResponse>
{
    private readonly Table _table;

    public TableDimensionsQueryHandler(Table table)
    {
        _table = table;
    }

    public Task<TableDimensionsQueryResponse> Handle(TableDimensionsQuery query, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new TableDimensionsQueryResponse(_table.Length, _table.Width));
    }
}