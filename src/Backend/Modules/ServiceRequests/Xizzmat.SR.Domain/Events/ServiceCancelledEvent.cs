using System;
using System.Collections.Generic;
using System.Text;
using Xizzmat.SR.Domain.Primitives;

namespace Xizzmat.SR.Domain.Events;

public record ServiceCancelledEvent(Guid ServiceId, Guid CustomerId) : DomainEvent;
