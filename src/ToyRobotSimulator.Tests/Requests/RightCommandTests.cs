using AutoFixture;
using FluentAssertions;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using static FluentAssertions.FluentActions;

namespace ToyRobotSimulator.Tests.Requests;

public class RightCommandTests : BaseForRequestHandlerTests
{
    [Test]
    public async Task Handle_ToyRobotNotOnTable_ThrowsException()
    {
        var table = ObjectFactory.Build<Table>().Without(x => x.ToyRobot).Create();
        var handler = new RightCommandHandler(table);

        await Invoking(async () => await handler.Handle(new RightCommand())).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Toy robot is not on the table");
    }

    [TestCase(Orientation.North, Orientation.East)]
    [TestCase(Orientation.East, Orientation.South)]
    [TestCase(Orientation.South, Orientation.West)]
    [TestCase(Orientation.West, Orientation.North)]
    public async Task Handle_ToyRobotOnTable_TurnsToyRobotRight(Orientation startOrientation, Orientation expectedOrientation)
    {
        var placement = ObjectFactory.Build<Placement>().With(x => x.Orientation, startOrientation).Create();
        var table = CreateTableWithToyRobotAt(placement);
        var handler = new RightCommandHandler(table);

        var response = await handler.Handle(new RightCommand());

        table.ToyRobot.Placement!.Orientation.Should().Be(expectedOrientation);
    }
}