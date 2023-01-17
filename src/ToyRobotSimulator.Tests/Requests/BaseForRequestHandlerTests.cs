using AutoFixture;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests.Requests;

public abstract class BaseForRequestHandlerTests : BaseForTests
{
    protected Table CreateTableWithToyRobotAt(Placement placement)
    {
        var toyRobot = ObjectFactory.Build<ToyRobot>().With(x => x.Placement, placement).Create();
        var table = ObjectFactory.Build<Table>()
            .With(x => x.Length, 3)
            .With(x => x.Width, 3)
            .With(x => x.ToyRobot, toyRobot).Create();
        return table;
    }
}