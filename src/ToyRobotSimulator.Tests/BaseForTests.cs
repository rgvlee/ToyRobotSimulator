using AutoFixture;

namespace ToyRobotSimulator.Tests;

public abstract class BaseForTests
{
    protected readonly Fixture ObjectFactory;

    protected BaseForTests()
    {
        ObjectFactory = new Fixture();
    }
}