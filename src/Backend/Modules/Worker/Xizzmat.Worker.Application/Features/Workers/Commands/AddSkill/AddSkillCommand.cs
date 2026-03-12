using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddSkill;

public record AddSkillCommand(Guid WorkerId, string Name) : ICommand;
