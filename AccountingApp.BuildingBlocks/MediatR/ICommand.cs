using MediatR;

namespace AccountingApp.BuildingBlocks.MediatR;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
