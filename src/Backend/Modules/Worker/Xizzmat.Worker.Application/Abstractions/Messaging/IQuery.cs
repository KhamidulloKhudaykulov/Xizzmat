using MediatR;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
