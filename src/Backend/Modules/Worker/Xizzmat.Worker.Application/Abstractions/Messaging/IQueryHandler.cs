using Xizzmat.Worker.Domain.Shared;
using MediatR;

namespace Xizzmat.Worker.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }