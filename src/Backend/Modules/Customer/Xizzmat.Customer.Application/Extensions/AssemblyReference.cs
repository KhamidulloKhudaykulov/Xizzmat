using System.Reflection;

namespace Xizzmat.Customer.Application.Extensions;

public static class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
