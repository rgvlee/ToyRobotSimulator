using MediatR;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Command to place the toy robot on the table at X,Y facing North|South|East|West.
/// </summary>
public class PlacementCommand : IRequest
{
    public PlacementCommand(Point point, Orientation orientation)
    {
        Point = point;
        Orientation = orientation;
    }

    public Point Point { get; }
    public Orientation Orientation { get; }
}

public class PlacementCommandHandler : IRequestHandler<PlacementCommand>
{
    private readonly Table _table;

    public PlacementCommandHandler(Table table)
    {
        _table = table;
    }

    public Task<Unit> Handle(PlacementCommand command, CancellationToken cancellationToken = default)
    {
        var x = command.Point.X;
        var y = command.Point.Y;

        if (x < 0 || x >= _table.Width || y < 0 || y >= _table.Length)
            throw new InvalidOperationException($"Toy robot cannot be placed at {x},{y} facing {command.Orientation.ToString().ToLower()} as it would fall off the table");

        if (_table.ToyRobot.Placement is null)
        {
            _table.ToyRobot.Placement = new Placement(new Point(x, y), command.Orientation);
        }
        else
        {
            _table.ToyRobot.Placement.Point.X = x;
            _table.ToyRobot.Placement.Point.Y = y;
            _table.ToyRobot.Placement.Orientation = command.Orientation;
        }

        return Task.FromResult(Unit.Value);
    }
}