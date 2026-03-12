using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Xizzmat.SR.Domain.Shared;

namespace Xizzmat.SR.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }