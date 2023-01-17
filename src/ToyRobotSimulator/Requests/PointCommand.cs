using MediatR;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Command to place the toy robot on the table at X,Y facing the same direction as before.
/// </summary>
public class PointCommand : IRequest
{
    public PointCommand(Point point)
    {
        Point = point;
    }

    public Point Point { get; }
}

public class PointCommandHandler : IRequestHandler<PointCommand>
{
    private readonly Table _table;

    public PointCommandHandler(Table table)
    {
        _table = table;
    }

    public Task<Unit> Handle(PointCommand command, CancellationToken cancellationToken = default)
    {
        if (_table.ToyRobot.Placement is null) throw new InvalidOperationException("Toy robot is not on the table");

        var existingPlacement = _table.ToyRobot.Placement;

        var x = command.Point.X;
        var y = command.Point.Y;

        if (x < 0 || x >= _table.Width || y < 0 || y >= _table.Length) throw new InvalidOperationException($"Toy robot cannot be placed at {x},{y} as it would fall off the table");

        existingPlacement.Point.X = x;
        existingPlacement.Point.Y = y;

        return Task.FromResult(Unit.Value);
    }
}