using MediatR;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
