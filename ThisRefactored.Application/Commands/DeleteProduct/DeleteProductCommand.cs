using MediatR;

namespace ThisRefactored.Application.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;