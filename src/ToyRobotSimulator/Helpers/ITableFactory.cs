using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Helpers;

/// <summary>
///     Creates tables.
/// </summary>
public interface ITableFactory
{
    Table Create();
}