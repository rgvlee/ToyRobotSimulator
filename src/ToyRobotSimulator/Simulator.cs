using MediatR;
using ToyRobotSimulator.Helpers;
using ToyRobotSimulator.Requests;
using ToyRobotSimulator.Responses;

namespace ToyRobotSimulator;

/// <summary>
///     Acts as the mediator between client and server.
/// </summary>
public class Simulator
{
    private readonly IMediator _mediator;

    public Simulator(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Processes a client request.
    /// </summary>
    /// <param name="request">The client request.</param>
    /// <returns>Commands return null, queries return a response.</returns>
    public async Task<object?> ProcessRequestAsync(string request)
    {
        return await _mediator.Send(Request.Parse(request));
    }

    /// <summary>
    ///     Obtains the table dimensions.
    /// </summary>
    /// <returns>The table dimensions.</returns>
    public async Task<TableDimensionsQueryResponse?> GetTableDimensionsAsync()
    {
        return await _mediator.Send(new TableDimensionsQuery());
    }
}