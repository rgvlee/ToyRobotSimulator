namespace ToyRobotSimulator.Models;

public class ToyRobot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Placement? Placement { get; set; }
}