using MediatR;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Requests;

/// <summary>
///     Command to turn the toy robot 90 degrees right.
/// </summary>
public class RightCommand : IRequest { }

public class RightCommandHandler : IRequestHandler<RightCommand>
{
    private readonly Table _table;

    public RightCommandHandler(Table table)
    {
        _table = table;
    }

    public Task<Unit> Handle(RightCommand command, CancellationToken cancellationToken = default)
    {
        if (_table.ToyRobot.Placement is null) throw new InvalidOperationException("Toy robot is not on the table");

        var existingPlacement = _table.ToyRobot.Placement;

        var orientation = existingPlacement.Orientation switch
        {
            Orientation.North => Orientation.East,
            Orientation.South => Orientation.West,
            Orientation.East => Orientation.South,
            Orientation.West => Orientation.North,
            _ => throw new NotImplementedException($"Unsupported orientation: {existingPlacement.Orientation}")
        };

        existingPlacement.Orientation = orientation;

        return Task.FromResult(Unit.Value);
    }
}