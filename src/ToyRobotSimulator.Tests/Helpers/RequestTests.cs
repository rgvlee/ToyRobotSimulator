using FluentAssertions;
using FluentAssertions.Execution;
using ToyRobotSimulator.Helpers;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;
using static FluentAssertions.FluentActions;

namespace ToyRobotSimulator.Tests.Helpers;

[TestFixture(Description = "Input parsing")]
public class RequestTests : BaseForTests
{
    [TestCase("move")]
    [TestCase("Move")]
    [TestCase("MoVe")]
    [TestCase("MOVE")]
    [TestCase(" MOVE")]
    [TestCase("MOVE ")]
    [TestCase(" MOVE ")]
    [TestCase("\tmove")]
    [TestCase("move\t")]
    [TestCase("\tmove\t")]
    public void Parse_ValidMoveRequest_CreatesMoveCommand(string input)
    {
        var request = Request.Parse(input);

        request.Should().BeOfType<MoveCommand>();
    }

    [TestCase("move 1")]
    public void Parse_InvalidMoveRequest_ThrowsException(string input)
    {
        Invoking(() => Request.Parse(input)).Should().ThrowExactly<InvalidOperationException>().WithMessage("Unsupported request");
    }

    [TestCase("left")]
    [TestCase("Left")]
    [TestCase("Left")]
    [TestCase("LEFT")]
    [TestCase(" LEFT")]
    [TestCase("LEFT ")]
    [TestCase(" LEFT ")]
    [TestCase("\tleft")]
    [TestCase("left\t")]
    [TestCase("\tleft\t")]
    public void Parse_ValidLeftRequest_CreatesLeftCommand(string input)
    {
        var request = Request.Parse(input);

        request.Should().BeOfType<LeftCommand>();
    }

    [TestCase("left 1")]
    public void Parse_InvalidLeftRequest_ThrowsException(string input)
    {
        Invoking(() => Request.Parse(input)).Should().ThrowExactly<InvalidOperationException>().WithMessage("Unsupported request");
    }

    [TestCase("right")]
    [TestCase("Right")]
    [TestCase("Right")]
    [TestCase("RIGHT")]
    [TestCase(" RIGHT")]
    [TestCase("RIGHT ")]
    [TestCase(" RIGHT ")]
    [TestCase("\tright")]
    [TestCase("right\t")]
    [TestCase("\tright\t")]
    public void Parse_ValidRightRequest_CreatesRightCommand(string input)
    {
        var request = Request.Parse(input);

        request.Should().BeOfType<RightCommand>();
    }

    [TestCase("right 1")]
    public void Parse_InvalidRightRequest_ThrowsException(string input)
    {
        Invoking(() => Request.Parse(input)).Should().ThrowExactly<InvalidOperationException>().WithMessage("Unsupported request");
    }

    [TestCase("report")]
    [TestCase("Report")]
    [TestCase("Report")]
    [TestCase("REPORT")]
    [TestCase(" REPORT")]
    [TestCase("REPORT ")]
    [TestCase(" REPORT ")]
    [TestCase("\treport")]
    [TestCase("report\t")]
    [TestCase("\treport\t")]
    public void Parse_ValidReportRequest_CreatesReportQuery(string input)
    {
        var request = Request.Parse(input);

        request.Should().BeOfType<ReportQuery>();
    }

    [TestCase("report 1")]
    public void Parse_InvalidReportRequest_ThrowsException(string input)
    {
        Invoking(() => Request.Parse(input)).Should().ThrowExactly<InvalidOperationException>().WithMessage("Unsupported request");
    }

    [TestCase("place {0},{1},{2}", 1, 2, Orientation.North)]
    [TestCase("place {0},{1},{2}", 1, 2, Orientation.South)]
    [TestCase("place {0},{1},{2}", 1, 2, Orientation.East)]
    [TestCase("place {0},{1},{2}", 1, 2, Orientation.West)]
    [TestCase("place {0} ,{1},{2}", 1, 2, Orientation.North)]
    [TestCase("place {0} ,{1} ,{2}", 1, 2, Orientation.North)]
    [TestCase("place {0} ,{1} ,{2} ", 1, 2, Orientation.North)]
    [TestCase(" place {0} ,{1} ,{2} ", 1, 2, Orientation.North)]
    [TestCase("place\t{0},{1},{2}", 1, 2, Orientation.North)]
    [TestCase("place\t{0}\t,{1},{2}", 1, 2, Orientation.North)]
    [TestCase("place\t{0}\t,{1}\t,{2}", 1, 2, Orientation.North)]
    [TestCase("place\t{0}\t,{1}\t,{2}\t", 1, 2, Orientation.North)]
    [TestCase("\tplace\t{0}\t,{1}\t,{2}\t", 1, 2, Orientation.North)]
    [TestCase("place {0} {1} {2}", 1, 2, Orientation.North)]
    public void Parse_ValidPlaceRequest_CreatesPlacementCommand(string inputTemplate, int expectedX, int expectedY, Orientation expectedOrientation)
    {
        var request = Request.Parse(string.Format(inputTemplate, expectedX, expectedY, expectedOrientation));

        using (new AssertionScope())
        {
            request.Should().BeOfType<PlacementCommand>();
            ((PlacementCommand)request).Point.X.Should().Be(expectedX);
            ((PlacementCommand)request).Point.Y.Should().Be(expectedY);
            ((PlacementCommand)request).Orientation.Should().Be(expectedOrientation);
        }
    }

    [TestCase("place {0},{1}", 1, 2)]
    [TestCase("place {0} ,{1}", 1, 2)]
    [TestCase("place {0} ,{1} ", 1, 2)]
    [TestCase(" place {0} ,{1} ", 1, 2)]
    [TestCase("place\t{0},{1}", 1, 2)]
    [TestCase("place\t{0}\t,{1}", 1, 2)]
    [TestCase("place\t{0}\t,{1}\t", 1, 2)]
    [TestCase("\tplace\t{0}\t,{1}\t", 1, 2)]
    [TestCase("place {0} {1}", 1, 2)]
    public void Parse_ValidPlaceRequest_CreatesPointCommand(string inputTemplate, int expectedX, int expectedY)
    {
        var request = Request.Parse(string.Format(inputTemplate, expectedX, expectedY));

        using (new AssertionScope())
        {
            request.Should().BeOfType<PointCommand>();
            ((PointCommand)request).Point.X.Should().Be(expectedX);
            ((PointCommand)request).Point.Y.Should().Be(expectedY);
        }
    }

    [TestCase("place")]
    [TestCase("place 1")]
    [TestCase("place one two")]
    [TestCase("place 1 2 3")]
    [TestCase("place north east")]
    [TestCase("place 1 north east")]
    [TestCase("place 1 2 north east")]
    [TestCase("place 1 2 3 north")]
    public void Parse_InvalidPlaceRequest_ThrowsException(string input)
    {
        Invoking(() => Request.Parse(input)).Should().ThrowExactly<InvalidOperationException>().WithMessage("Unsupported request");
    }
}