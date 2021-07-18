using System.Reflection;

namespace SPTarkov.TestModule.Utils.Patching
{
    public abstract class GenericPatch<T> where T : GenericPatch<T>
    {
        private MethodBase _targetMethod = null;

        public MethodBase TargetMethod
        {
            get
            {
                if (_targetMethod == null)
                {
                    _targetMethod = GetTargetMethod();
                }

                return _targetMethod;
            }
        }

        public MethodInfo Prefix { get; }

        public MethodInfo Postfix { get; }

        public MethodInfo Transpiler { get; }

        public MethodInfo Finalizer { get; }

        protected abstract MethodBase GetTargetMethod();

    }
}
