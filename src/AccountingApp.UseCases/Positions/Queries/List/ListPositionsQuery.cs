namespace AccountingApp.UseCases.Positions.Queries.List;

public record ListPositionsQuery : IQuery<Result<IEnumerable<PositionDto>>>;