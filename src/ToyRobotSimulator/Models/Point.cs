namespace ToyRobotSimulator.Models;

/// <summary>
///     Describes a point on a cartesian plane.
/// </summary>
public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}