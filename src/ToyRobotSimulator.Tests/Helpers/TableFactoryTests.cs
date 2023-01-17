using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Options;
using Moq;
using ToyRobotSimulator.Helpers;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Settings;

namespace ToyRobotSimulator.Tests.Helpers;

public class TableFactoryTests : BaseForTests
{
    [Test]
    public void Create_CreatesTable()
    {
        var settings = ObjectFactory.Create<TableSettings>();
        var settingsAccessor = Mock.Of<IOptions<TableSettings>>(x => x.Value == settings);
        var factory = new TableFactory(settingsAccessor);

        var table = factory.Create();

        using (new AssertionScope())
        {
            table.Should().BeOfType<Table>();

            table.Id.Should().NotBeEmpty();
            table.Length.Should().Be(settings.Length);
            table.Width.Should().Be(settings.Width);
            table.ToyRobot.Should().NotBeNull();

            table.ToyRobot.Id.Should().NotBeEmpty();
            table.ToyRobot.Placement.Should().BeNull();
        }
    }
}