using System.Runtime.CompilerServices;

namespace Qread.Tests.Unit.Generators;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}
