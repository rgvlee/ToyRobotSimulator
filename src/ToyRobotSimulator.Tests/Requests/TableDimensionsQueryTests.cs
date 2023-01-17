using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;

namespace ToyRobotSimulator.Tests.Requests;

public class TableDimensionsQueryTests : BaseForRequestHandlerTests
{
    [Test]
    public async Task Handle_ReturnsTableDimensions()
    {
        var table = ObjectFactory.Create<Table>();
        var handler = new TableDimensionsQueryHandler(table);

        var response = await handler.Handle(new TableDimensionsQuery());

        using (new AssertionScope())
        {
            response.Length.Should().Be(table.Length);
            response.Width.Should().Be(table.Width);
        }
    }
}