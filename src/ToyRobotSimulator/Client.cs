using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToyRobotSimulator;
using ToyRobotSimulator.Helpers;
using ToyRobotSimulator.Responses;
using ToyRobotSimulator.Settings;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder => builder.AddJsonFile(@"appsettings.json"))
    .ConfigureLogging(builder => builder.AddConsole())
    .ConfigureServices((builder, services) =>
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddOptions<TableSettings>()
            .Bind(builder.Configuration.GetSection("Table"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<Simulator>();

        services.AddScoped<ITableFactory, TableFactory>();

        services.AddScoped(provider => provider.GetRequiredService<ITableFactory>().Create());
    }).Build();

try
{
    host.Start();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

using var serviceScope = host.Services.CreateScope();
var simulator = serviceScope.ServiceProvider.GetRequiredService<Simulator>();
var tableDimensions = await simulator.GetTableDimensionsAsync();

Console.WriteLine(
    $@"
  _____             ____       _           _     ____  _                 _       _             
 |_   _|__  _   _  |  _ \ ___ | |__   ___ | |_  / ___|(_)_ __ ___  _   _| | __ _| |_ ___  _ __ 
   | |/ _ \| | | | | |_) / _ \| '_ \ / _ \| __| \___ \| | '_ ` _ \| | | | |/ _` | __/ _ \| '__|
   | | (_) | |_| | |  _ < (_) | |_) | (_) | |_   ___) | | | | | | | |_| | | (_| | || (_) | |   
   |_|\___/ \__, | |_| \_\___/|_.__/ \___/ \__| |____/|_|_| |_| |_|\__,_|_|\__,_|\__\___/|_|   
            |___/                                                                              

Welcome to the Toy Robot Simulator. Place your toy robot on the table and then use one of the following instructions to move it around.

The table is effectively a {tableDimensions!.Width}x{tableDimensions!.Length} {(tableDimensions!.Length == tableDimensions!.Width ? "square" : "rectangle")} on a cartesian plane with the south west corner at 0,0 and the north east corner at {tableDimensions!.Width - 1},{tableDimensions!.Length - 1}. The toy robot can only be placed on the table and cannot be moved off the table. There are no obstructions on the table, the toy robot is free to roam around at your discretion.

Supported instructions:

Place X,Y,[North|South|East|West]
Places the toy robot on the table at X,Y facing North|South|East|West. You must complete this step before further instructions can be processed.

Place X,Y
Places the toy robot on the table at X,Y facing the same direction as before

Move
Moves the toy robot one unit forward in the direction it is facing

Left
Turns the toy robot 90 degrees left

Right
Turns the toy robot 90 degrees right

Report
Prints the toy robot position and direction

Quit
Shuts down the simulation.
");

do
{
    Console.Write("Instruction: ");

    var request = Console.ReadLine();

    // ReadLine can return null, such as when ctrl+c is pressed
    // Cancel key press presents an interesting challenge when used with ReadLine
    // For brevity, if ReadLine returns null we treat it as cancel key press and quit
    if (request is null || request.Trim().Equals("Quit", StringComparison.CurrentCultureIgnoreCase))
        break;

    try
    {
        var response = await simulator.ProcessRequestAsync(request);

        if (response is ReportQueryResponse reportQueryResponse)
            Console.WriteLine($"Toy robot is located at {reportQueryResponse.X},{reportQueryResponse.Y} facing {reportQueryResponse.Orientation.ToString().ToLower()}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
} while (true);