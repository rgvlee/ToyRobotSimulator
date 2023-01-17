using Microsoft.Extensions.Options;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Settings;

namespace ToyRobotSimulator.Helpers;

/// <summary>
///     Creates tables.
/// </summary>
public class TableFactory : ITableFactory
{
    private readonly TableSettings _tableOptions;

    public TableFactory(IOptions<TableSettings> tableOptionsAccessor)
    {
        _tableOptions = tableOptionsAccessor.Value;
    }

    /// <summary>
    ///     Creates a square or rectangle table with length/width as specified in app settings.
    /// </summary>
    /// <returns>A table.</returns>
    public Table Create()
    {
        return new Table(_tableOptions.Length, _tableOptions.Width);
    }
}