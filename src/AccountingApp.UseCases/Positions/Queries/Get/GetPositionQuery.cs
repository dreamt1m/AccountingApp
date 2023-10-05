namespace AccountingApp.UseCases.Positions.Queries.Get;

public record GetPositionQuery(Guid PositionId) : IQuery<Result<PositionDto>>;