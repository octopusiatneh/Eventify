using System.Reflection;

namespace Eventify.Modules.Ticketing.Presentation;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
