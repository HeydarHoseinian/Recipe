using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.BOL
{
    public class BOLLayer
    {
        private Dictionary<Type, Type> RegisteredServiceMap { get; set; }
        private Dictionary<Type, object> RegisteredServiceSingletoneInstances { get; set; }
        private BOLLayer()
        {
            RegisteredServiceMap = new Dictionary<Type, Type>();
            RegisteredServiceSingletoneInstances = new Dictionary<Type, object>();
        }
        public static BOLLayer instance;
        public static BOLLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BOLLayer();
                }
                return instance;
            }
        }
        public bool InitializeApplication()
        {
            RegisteredServiceMap.Add(typeof(IRecipeDataProvider), typeof(SimpleFileBaseRecipeDataProvider));
            return true;
        }
        public IT GetService<IT>()
        {
            Type it = typeof(IT);
            var st = GetServiceType(it);
            if (st != null)
            {
                object service = null;
                if (RegisteredServiceSingletoneInstances.TryGetValue(st, out service))
                {
                    return (IT)service;
                }
                else
                {
                    IT si = (IT)Activator.CreateInstance(st);
                    if (si != null)
                    {
                        RegisteredServiceSingletoneInstances.Add(st, service);
                    }
                    return si;
                }
            }
            throw new Exception("no Service Type Registered For This Type" + typeof(IT).FullName);
        }
        private Type GetServiceType(Type it)
        {
            Type serviceType;
            if (RegisteredServiceMap.TryGetValue(it, out serviceType))
            {
                return serviceType;
            }
            else
            {
                throw new Exception("Type " + it.FullName + " Not Registered");
            }
        }
    }
}
