using MediatR;

namespace ThisRefactored.Application.Commands.DeleteProductOption;

public record DeleteProductOptionCommand(Guid Id, Guid ProductId) : IRequest;