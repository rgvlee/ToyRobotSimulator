using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Moq;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using ToyRobotSimulator.Responses;

namespace ToyRobotSimulator.Tests;

public class SimulatorTests : BaseForTests
{
    [Test]
    public async Task ProcessRequestAsync_ValidPlacementRequest_SendsRequestToMediatr()
    {
        var mediatrMock = new Mock<IMediator>();
        var simulator = new Simulator(mediatrMock.Object);

        var response = await simulator.ProcessRequestAsync("Place 1,2,North");

        mediatrMock.Verify(x =>
                x.Send(
                    It.Is<object>(y =>
                        y.GetType() == typeof(PlacementCommand) &&
                        ((PlacementCommand)y).Point.X == 1 &&
                        ((PlacementCommand)y).Point.Y == 2 &&
                        ((PlacementCommand)y).Orientation == Orientation.North),
                    It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task GetTableDimensionsAsync_SendsRequestToMediatr()
    {
        var expectedResponse = ObjectFactory.Create<TableDimensionsQueryResponse>();
        var mediatrMock = new Mock<IMediator>();
        mediatrMock.Setup(x => x.Send(
                It.IsAny<IRequest<TableDimensionsQueryResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => expectedResponse);
        var simulator = new Simulator(mediatrMock.Object);

        var response = await simulator.GetTableDimensionsAsync();

        using (new AssertionScope())
        {
            response.Should().Be(expectedResponse);
            mediatrMock.Verify(x =>
                    x.Send(
                        It.IsAny<IRequest<TableDimensionsQueryResponse>>(),
                        It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}