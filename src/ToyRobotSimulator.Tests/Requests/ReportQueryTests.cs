using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using static FluentAssertions.FluentActions;

namespace ToyRobotSimulator.Tests.Requests;

public class ReportQueryTests : BaseForRequestHandlerTests
{
    [Test]
    public async Task Handle_ToyRobotNotOnTable_ThrowsException()
    {
        var table = ObjectFactory.Build<Table>().Without(x => x.ToyRobot).Create();
        var handler = new ReportQueryHandler(table);

        await Invoking(async () => await handler.Handle(new ReportQuery())).Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Toy robot is not on the table");
    }

    [Test]
    public async Task Handle_ToyRobotOnTable_ReturnsToyRobotPlacement()
    {
        var table = ObjectFactory.Build<Table>().With(x => x.Length, 3).With(x => x.Width, 3).Create();
        var handler = new ReportQueryHandler(table);

        var response = await handler.Handle(new ReportQuery());

        using (new AssertionScope())
        {
            response.X.Should().Be(table.ToyRobot.Placement!.Point.X);
            response.Y.Should().Be(table.ToyRobot.Placement!.Point.Y);
            response.Orientation.Should().Be(table.ToyRobot.Placement!.Orientation);
        }
    }
}