using System.Reflection;

namespace Eventify.Modules.Events.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
