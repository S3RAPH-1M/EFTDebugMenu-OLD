using SPTarkov.TestModule.Utils.Hook;
using NLog.Targets;

namespace SPTarkov.TestModule.Hook
{
    [Target("SPTarkov.TestModule")]
    public sealed class Target : TargetWithLayout
    {
        public Target() => Loader<Instance>.Load();
    }
}
