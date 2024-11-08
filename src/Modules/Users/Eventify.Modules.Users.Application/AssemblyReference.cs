using System.Reflection;

namespace Eventify.Modules.Users.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
