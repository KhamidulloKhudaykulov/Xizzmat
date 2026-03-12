using Xizzmat.Customer.Domain.Shared;
using MediatR;

namespace Xizzmat.Customer.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
