using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }