namespace ToyRobotSimulator.Responses;

public class TableDimensionsQueryResponse
{
    public TableDimensionsQueryResponse(int length, int width)
    {
        Length = length;
        Width = width;
    }

    public int Length { get; }
    public int Width { get; }
}