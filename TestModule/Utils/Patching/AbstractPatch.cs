using System.Reflection;

namespace SPTarkov.TestModule.Utils.Patching
{
    public abstract class AbstractPatch
    {
        public string methodName;
        public BindingFlags flags;

        public abstract MethodInfo TargetMethod();
    }
}
