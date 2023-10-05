using MediatR;

namespace AccountingApp.BuildingBlocks.MediatR;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
