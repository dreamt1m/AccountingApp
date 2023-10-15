namespace AccountingApp.UseCases.Bonuses.Queries.List;

public record ListBonusesQuery(Guid EmployeeId) : IQuery<Result<IEnumerable<BonusDto>>>;