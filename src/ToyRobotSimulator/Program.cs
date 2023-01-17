using ToyRobotSimulator;

Console.WriteLine("Valid commands:");
Console.WriteLine("\tPLACE X,Y,[NORTH|SOUTH|EAST|WEST]");
Console.WriteLine("\tMOVE");
Console.WriteLine("\tLEFT");
Console.WriteLine("\tRIGHT");
Console.WriteLine("\tREPORT");
Console.WriteLine("\tQUIT");

var toyRobot = new ToyRobot();
var input = string.Empty;

do
{
    //accept input
    input = Console.ReadLine() ?? string.Empty;

    if (input.Equals("QUIT", StringComparison.OrdinalIgnoreCase)) break;

    //parse input and execute command
    if (input.StartsWith("PLACE ", StringComparison.OrdinalIgnoreCase))
    {
        //obtain the arguments
        var arguments = input.Substring("PLACE ".Length).Split(',', StringSplitOptions.TrimEntries);

        if (arguments.Length != 2 && arguments.Length != 3)
        {
            Console.WriteLine("Invalid input");
            continue;
        }

        if (arguments.Length == 2 && !toyRobot.Position.HasValue)
        {
            Console.WriteLine("Toy robot has not been placed on the table");
            continue;
        }

        int x = default;
        int y = default;
        Direction direction = default;
        if (!int.TryParse(arguments[0], out x) ||
            !int.TryParse(arguments[1], out y) ||
            (arguments.Length == 3 && !Enum.TryParse(arguments[2], true, out direction)))
        {
            Console.WriteLine("Invalid input");
            continue;
        }

        if (arguments.Length == 2) toyRobot.Place(x, y);
        else toyRobot.Place(x, y, direction);
        continue;
    }

    if (!toyRobot.Position.HasValue)
    {
        Console.WriteLine("Toy robot has not been placed on the table");
        continue;
    }

    if (input.Equals("LEFT", StringComparison.OrdinalIgnoreCase)) toyRobot.Left();
    else if (input.Equals("RIGHT", StringComparison.OrdinalIgnoreCase)) toyRobot.Right();
    else if (input.Equals("MOVE", StringComparison.OrdinalIgnoreCase)) toyRobot.Move();
    else if (input.Equals("REPORT", StringComparison.OrdinalIgnoreCase)) Console.WriteLine(toyRobot);
    else Console.WriteLine("Invalid command");
} while (true);