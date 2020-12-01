using EjderyaFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.Core.Aspects.Pastsharp.CacheAspects
{
    [Serializable]
    public class CacheRemoveAspect:OnMethodBoundaryAspect
    {
        private Type _cacheType;
        private string _pattern;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        public CacheRemoveAspect(string pattern, Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }
        public override void OnException(MethodExecutionArgs args)
        {
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern)
                ? string.Format("{0}.{1}.*", args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name)
                : _pattern);
        }
    }
}
