using System.Reflection;

namespace MSP.Courses.Infra;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}