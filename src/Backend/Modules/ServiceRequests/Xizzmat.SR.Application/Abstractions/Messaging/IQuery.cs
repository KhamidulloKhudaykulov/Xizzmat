using Xizzmat.SR.Domain.Shared;
using MediatR;

namespace Xizzmat.SR.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
