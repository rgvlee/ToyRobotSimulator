using System.ComponentModel.DataAnnotations;

namespace ToyRobotSimulator.Settings;

public class TableSettings
{
    [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public int Length { get; set; }

    [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public int Width { get; set; }
}