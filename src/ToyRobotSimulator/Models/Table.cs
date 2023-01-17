namespace ToyRobotSimulator.Models;

public class Table
{
    public Table(int length, int width)
    {
        Length = length;
        Width = width;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public int Length { get; set; }
    public int Width { get; set; }
    public ToyRobot ToyRobot { get; set; } = new();
}