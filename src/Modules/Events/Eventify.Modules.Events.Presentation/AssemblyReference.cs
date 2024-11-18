using System.Reflection;

namespace Eventify.Modules.Events.Presentation;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
