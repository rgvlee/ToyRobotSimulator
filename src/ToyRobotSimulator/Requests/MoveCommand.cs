using MediatR;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Command to move the toy robot one unit forward in the direction it is facing.
/// </summary>
public class MoveCommand : IRequest { }

public class MoveCommandHandler : IRequestHandler<MoveCommand>
{
    private readonly Table _table;

    public MoveCommandHandler(Table table)
    {
        _table = table;
    }

    public Task<Unit> Handle(MoveCommand command, CancellationToken cancellationToken = default)
    {
        if (_table.ToyRobot.Placement is null) throw new InvalidOperationException("Toy robot is not on the table");

        var existingPlacement = _table.ToyRobot.Placement;

        var x = existingPlacement.Point.X;
        var y = existingPlacement.Point.Y;

        switch (existingPlacement.Orientation)
        {
            case Orientation.North:
                y += 1;
                break;
            case Orientation.South:
                y -= 1;
                break;
            case Orientation.East:
                x += 1;
                break;
            case Orientation.West:
                x -= 1;
                break;
            default:
                throw new NotImplementedException($"Unsupported orientation: {existingPlacement.Orientation}");
        }

        if (x < 0 || x >= _table.Width || y < 0 || y >= _table.Length) throw new InvalidOperationException($"Toy robot cannot be moved to {x},{y} as it would fall off the table");

        existingPlacement.Point.X = x;
        existingPlacement.Point.Y = y;

        return Task.FromResult(Unit.Value);
    }
}