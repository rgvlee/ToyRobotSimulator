using MediatR;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Command to turn the toy robot 90 degrees left.
/// </summary>
public class LeftCommand : IRequest { }

public class LeftCommandHandler : IRequestHandler<LeftCommand>
{
    private readonly Table _table;

    public LeftCommandHandler(Table table)
    {
        _table = table;
    }

    public Task<Unit> Handle(LeftCommand command, CancellationToken cancellationToken = default)
    {
        if (_table.ToyRobot.Placement is null) throw new InvalidOperationException("Toy robot is not on the table");

        var existingPlacement = _table.ToyRobot.Placement;

        var orientation = existingPlacement.Orientation switch
        {
            Orientation.North => Orientation.West,
            Orientation.South => Orientation.East,
            Orientation.East => Orientation.North,
            Orientation.West => Orientation.South,
            _ => throw new NotImplementedException($"Unsupported orientation: {existingPlacement.Orientation}")
        };

        existingPlacement.Orientation = orientation;

        return Task.FromResult(Unit.Value);
    }
}