using Xizzmat.Customer.Domain.Shared;
using MediatR;

namespace Xizzmat.Customer.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }