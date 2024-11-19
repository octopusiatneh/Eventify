using System.Reflection;

namespace Eventify.Modules.Ticketing.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
