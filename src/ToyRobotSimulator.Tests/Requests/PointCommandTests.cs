using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using static FluentAssertions.FluentActions;

namespace ToyRobotSimulator.Tests.Requests;

public class PointCommandTests : BaseForRequestHandlerTests
{
    [Test]
    public async Task Handle_ToyRobotNotOnTable_ThrowsException()
    {
        var table = ObjectFactory.Build<Table>().Without(x => x.ToyRobot).Create();
        var handler = new PointCommandHandler(table);

        await Invoking(async () => await handler.Handle(new PointCommand(new Point(1, 2)))).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Toy robot is not on the table");
    }

    [Test]
    public async Task Handle_ToyRobotOnTable_PlacesToyRobotAtPoint()
    {
        var startOrientation = ObjectFactory.Create<Orientation>();
        var startPoint = ObjectFactory.Build<Point>().With(x => x.X, 1).With(x => x.Y, 1).Create();
        var expectedPoint = ObjectFactory.Build<Point>().With(x => x.X, 2).With(x => x.Y, 2).Create();
        var placement = ObjectFactory.Build<Placement>().With(x => x.Point, startPoint).With(x => x.Orientation, startOrientation).Create();
        var table = CreateTableWithToyRobotAt(placement);
        var handler = new PointCommandHandler(table);

        var response = await handler.Handle(new PointCommand(expectedPoint));

        using (new AssertionScope())
        {
            table.ToyRobot.Placement!.Point.X.Should().Be(expectedPoint.X);
            table.ToyRobot.Placement!.Point.Y.Should().Be(expectedPoint.Y);
            table.ToyRobot.Placement!.Orientation.Should().Be(startOrientation);
        }
    }

    [TestCase(2, 3)]
    [TestCase(3, 3)]
    [TestCase(0, -1)]
    [TestCase(-1, -1)]
    public async Task Handle_PlaceToyRobotOffTheTable_ThrowsException(int expectedX, int expectedY)
    {
        var startOrientation = ObjectFactory.Create<Orientation>();
        var startPoint = ObjectFactory.Build<Point>().With(x => x.X, 1).With(x => x.Y, 1).Create();
        var expectedPoint = ObjectFactory.Build<Point>().With(x => x.X, expectedX).With(x => x.Y, expectedY).Create();
        var placement = ObjectFactory.Build<Placement>().With(x => x.Point, startPoint).With(x => x.Orientation, startOrientation).Create();
        var table = CreateTableWithToyRobotAt(placement);
        var handler = new PointCommandHandler(table);

        await Invoking(async () => await handler.Handle(new PointCommand(expectedPoint))).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage($"Toy robot cannot be placed at {expectedX},{expectedY} as it would fall off the table");
    }
}