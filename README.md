# Toy robot simulator

That's right, here we have yet another toy robot simulator.

This version is a .NET 6 console application. Valid instructions provided by the user place and move a virtual toy robot
on/around a virtual table.

## Objective

The scenario is a common competency
exercise, [it's been around for a long time](https://joneaves.wordpress.com/2014/07/21/toy-robot-coding-test/). That
being said this repository represents my first go at it and is my own work.

The objective is in the name, competency. I've done a handful of exercises like this before and I like to target the
things that I look for myself when I'm reviewing candidates:

- Knowledge of common concepts (e.g., SOLID principles and design patterns) and architectures, and when to use them
- The solution must be simple; structured; maintainable; testable; and other buzz words that senior developers like to
  throw around

And it has to work, of course.

## Running the solution

Clone this repository, open `src\ToyRobotSimulator.sln` in Visual Studio (or if you're cool like me, Rider), run.

Alternatively you can use the CLI; from the repository folder:

```text
dotnet build src --configuration Release
cd src\ToyRobotSimulator\bin\Release\net6.0
ToyRobotSimulator.exe
```

Tests can be run from within Visual Studio. Or if you prefer to use the CLI:

```text
dotnet test src --configuration Release
```

As a .NET 6 project the [.NET 6 runtime is a prerequisite](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## Usage

All of the information you need is provided when the simulator starts.

## Configuration

The included `appsettings.json` allows you to configure the table size from 1x1 to 100x100 and everything in between,
should you wish.

By default it is set to 6x6. If the toy robot was sentient it'd probably object to a 1x1 table. But the option is there
and you can spin it around til you've had enough.
