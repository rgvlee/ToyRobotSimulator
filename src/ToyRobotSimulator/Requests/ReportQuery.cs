using MediatR;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Responses;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Query that returns the toy robot placement on the table.
/// </summary>
public class ReportQuery : IRequest<ReportQueryResponse> { }

public class ReportQueryHandler : IRequestHandler<ReportQuery, ReportQueryResponse>
{
    private readonly Table _table;

    public ReportQueryHandler(Table table)
    {
        _table = table;
    }

    public Task<ReportQueryResponse> Handle(ReportQuery query, CancellationToken cancellationToken = default)
    {
        if (_table.ToyRobot.Placement is null) throw new InvalidOperationException("Toy robot is not on the table");

        return Task.FromResult(new ReportQueryResponse(_table.ToyRobot.Placement.Point.X, _table.ToyRobot.Placement.Point.Y, _table.ToyRobot.Placement.Orientation));
    }
}