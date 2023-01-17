using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Responses;

public class ReportQueryResponse
{
    public ReportQueryResponse(int x, int y, Orientation orientation)
    {
        X = x;
        Y = y;
        Orientation = orientation;
    }

    public int X { get; }
    public int Y { get; }
    public Orientation Orientation { get; }
}