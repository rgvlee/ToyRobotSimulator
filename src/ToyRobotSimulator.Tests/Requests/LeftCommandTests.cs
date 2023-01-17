using AutoFixture;
using FluentAssertions;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using static FluentAssertions.FluentActions;

namespace ToyRobotSimulator.Tests.Requests;

public class LeftCommandTests : BaseForRequestHandlerTests
{
    [Test]
    public async Task Handle_ToyRobotNotOnTable_ThrowsException()
    {
        var table = ObjectFactory.Build<Table>().Without(x => x.ToyRobot).Create();
        var handler = new LeftCommandHandler(table);

        await Invoking(async () => await handler.Handle(new LeftCommand())).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Toy robot is not on the table");
    }

    [TestCase(Orientation.North, Orientation.West)]
    [TestCase(Orientation.West, Orientation.South)]
    [TestCase(Orientation.South, Orientation.East)]
    [TestCase(Orientation.East, Orientation.North)]
    public async Task Handle_ToyRobotOnTable_TurnsToyRobotLeft(Orientation startOrientation, Orientation expectedOrientation)
    {
        var placement = ObjectFactory.Build<Placement>().With(x => x.Orientation, startOrientation).Create();
        var table = CreateTableWithToyRobotAt(placement);
        var handler = new LeftCommandHandler(table);

        var response = await handler.Handle(new LeftCommand());

        table.ToyRobot.Placement!.Orientation.Should().Be(expectedOrientation);
    }
}