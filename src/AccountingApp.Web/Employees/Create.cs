using AccountingApp.UseCases.Employees.Commands.Create;
using Ardalis.Result;
using FastEndpoints;
using MediatR;

namespace AccountingApp.Web.Employees
{
    public class Create : Endpoint<CreateEmployeeRequest, CreateEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post(CreateEmployeeRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreateEmployeeRequest
                {
                    Name = "Tom",
                    PositionId = Guid.Empty
                };
            });
        }

        public override async Task HandleAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateEmployeeCommand(
                request.Name!,
                request.PositionId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.Status == ResultStatus.Error)
            {
                foreach (var error in result.Errors)
                {
                    AddError(error);
                }
            }

            if (result.IsSuccess)
            {
                Response = new CreateEmployeeResponse(result.Value);
            }
        }
    }
}