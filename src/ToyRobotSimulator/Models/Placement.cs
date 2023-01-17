namespace ToyRobotSimulator.Models;

/// <summary>
///     Describes the placement of an entity on a cartesian plane.
/// </summary>
public class Placement
{
    public Placement(Point point, Orientation orientation)
    {
        Point = point;
        Orientation = orientation;
    }

    public Point Point { get; set; }
    public Orientation Orientation { get; set; }
}