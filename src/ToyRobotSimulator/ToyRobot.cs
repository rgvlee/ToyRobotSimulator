namespace ToyRobotSimulator;

public class ToyRobot
{
    public Direction? Direction { get; private set; }
    public Position? Position { get; private set; }

    public void Left()
    {
        Direction = Direction switch
        {
            ToyRobotSimulator.Direction.North => ToyRobotSimulator.Direction.West,
            ToyRobotSimulator.Direction.South => ToyRobotSimulator.Direction.East,
            ToyRobotSimulator.Direction.East => ToyRobotSimulator.Direction.North,
            ToyRobotSimulator.Direction.West => ToyRobotSimulator.Direction.South,
            _ => throw new InvalidOperationException()
        };
    }

    public void Right()
    {
        Direction = Direction switch
        {
            ToyRobotSimulator.Direction.North => ToyRobotSimulator.Direction.East,
            ToyRobotSimulator.Direction.South => ToyRobotSimulator.Direction.West,
            ToyRobotSimulator.Direction.East => ToyRobotSimulator.Direction.South,
            ToyRobotSimulator.Direction.West => ToyRobotSimulator.Direction.North,
            _ => throw new InvalidOperationException()
        };
    }

    public void Move()
    {
        if (!Position.HasValue) throw new InvalidOperationException();

        var x = Position.Value.X;
        var y = Position.Value.Y;

        switch (Direction)
        {
            case ToyRobotSimulator.Direction.North:
                x += 1;
                break;
            case ToyRobotSimulator.Direction.South:
                x -= 1;
                break;
            case ToyRobotSimulator.Direction.East:
                y += 1;
                break;
            case ToyRobotSimulator.Direction.West:
                y -= 1;
                break;
            default:
                throw new InvalidOperationException();
        }

        if (x < 0) x = 0;
        else if (x > 5) x = 5;
        if (y < 0) y = 0;
        else if (y > 5) y = 5;

        Position = new Position(x, y);
    }

    public void Place(int x, int y, Direction direction)
    {
        Position = new Position(x, y);
        Direction = direction;
    }

    public void Place(int x, int y)
    {
        Position = new Position(x, y);
    }

    public override string ToString()
    {
        if (!Position.HasValue) throw new InvalidOperationException();
        return $"X: {Position.Value.X}, Y: {Position.Value.Y}, Direction: {Direction}";
    }
}