using System.Reflection;

namespace Xizzmat.Worker.Infrastructure.Extensions;

public static class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
