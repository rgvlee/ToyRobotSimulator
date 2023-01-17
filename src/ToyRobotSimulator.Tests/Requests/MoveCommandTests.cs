using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using static FluentAssertions.FluentActions;

namespace ToyRobotSimulator.Tests.Requests;

public class MoveCommandTests : BaseForRequestHandlerTests
{
    [Test]
    public async Task Handle_ToyRobotNotOnTable_ThrowsException()
    {
        var table = ObjectFactory.Build<Table>().Without(x => x.ToyRobot).Create();
        var handler = new MoveCommandHandler(table);

        await Invoking(async () => await handler.Handle(new MoveCommand())).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Toy robot is not on the table");
    }

    [TestCase(1, 1, Orientation.North, 1, 2)]
    [TestCase(1, 1, Orientation.East, 2, 1)]
    [TestCase(1, 1, Orientation.South, 1, 0)]
    [TestCase(1, 1, Orientation.West, 0, 1)]
    public async Task Handle_ToyRobotOnTable_MovesToyRobotForward(int startX, int startY, Orientation startOrientation, int expectedX, int expectedY)
    {
        var point = ObjectFactory.Build<Point>().With(x => x.X, startX).With(x => x.Y, startY).Create();
        var placement = ObjectFactory.Build<Placement>().With(x => x.Point, point).With(x => x.Orientation, startOrientation).Create();
        var table = CreateTableWithToyRobotAt(placement);
        var handler = new MoveCommandHandler(table);

        var response = await handler.Handle(new MoveCommand());

        using (new AssertionScope())
        {
            table.ToyRobot.Placement!.Point.X.Should().Be(expectedX);
            table.ToyRobot.Placement!.Point.Y.Should().Be(expectedY);
            table.ToyRobot.Placement!.Orientation.Should().Be(startOrientation);
        }
    }

    [TestCase(3, 3, Orientation.North, 3, 4)]
    [TestCase(3, 3, Orientation.East, 4, 3)]
    [TestCase(0, 0, Orientation.South, 0, -1)]
    [TestCase(0, 0, Orientation.West, -1, 0)]
    public async Task Handle_ToyRobotOnEdgeOfTable_ThrowsException(int startX, int startY, Orientation startOrientation, int expectedX, int expectedY)
    {
        var point = ObjectFactory.Build<Point>().With(x => x.X, startX).With(x => x.Y, startY).Create();
        var placement = ObjectFactory.Build<Placement>().With(x => x.Point, point).With(x => x.Orientation, startOrientation).Create();
        var table = CreateTableWithToyRobotAt(placement);
        var handler = new MoveCommandHandler(table);

        await Invoking(async () => await handler.Handle(new MoveCommand())).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage($"Toy robot cannot be moved to {expectedX},{expectedY} as it would fall off the table");
    }
}