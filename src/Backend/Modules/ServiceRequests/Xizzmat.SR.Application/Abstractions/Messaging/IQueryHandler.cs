using Xizzmat.SR.Domain.Shared;
using MediatR;

namespace Xizzmat.SR.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }