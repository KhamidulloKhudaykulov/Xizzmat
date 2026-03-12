using System.Reflection;

namespace Xizzmat.Customer.Infrastructure.Extensions;

public static class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
