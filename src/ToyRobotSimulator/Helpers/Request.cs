using System.Text.RegularExpressions;
using MediatR;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Requests;

namespace ToyRobotSimulator.Helpers;

public static class Request
{
    /// <summary>
    ///     Parses a client request into a command or query that can be executed.
    /// </summary>
    /// <param name="request">The client request.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">The request is not supported.</exception>
    public static IBaseRequest Parse(string request)
    {
        request = request.Trim();

        if (Regex.IsMatch(request, @"^Place\s+", RegexOptions.IgnoreCase))
        {
            var arguments = Regex.Replace(request, @"\s", ",").Split(",", StringSplitOptions.RemoveEmptyEntries);

            switch (arguments.Length)
            {
                case 3 when int.TryParse(arguments[1], out var x) && int.TryParse(arguments[2], out var y):
                    return new PointCommand(new Point(x, y));
                case 4 when
                    int.TryParse(arguments[1], out var x) &&
                    int.TryParse(arguments[2], out var y) &&
                    Enum.GetValues<Orientation>().Any(z => arguments[3].Equals(z.ToString(), StringComparison.CurrentCultureIgnoreCase)):
                    return new PlacementCommand(new Point(x, y), Enum.Parse<Orientation>(arguments[3], true));
            }
        }
        else if (request.Equals("Move", StringComparison.CurrentCultureIgnoreCase))
        {
            return new MoveCommand();
        }
        else if (request.Equals("Left", StringComparison.CurrentCultureIgnoreCase))
        {
            return new LeftCommand();
        }
        else if (request.Equals("Right", StringComparison.CurrentCultureIgnoreCase))
        {
            return new RightCommand();
        }
        else if (request.Equals("Report", StringComparison.CurrentCultureIgnoreCase))
        {
            return new ReportQuery();
        }

        throw new InvalidOperationException("Unsupported request");
    }
}