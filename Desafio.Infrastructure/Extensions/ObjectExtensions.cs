using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static TTarget Copy<TTarget>(this object currentObj)
        {
            if (currentObj == null)
                return default(TTarget);

            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            var result = Activator.CreateInstance<TTarget>();

            foreach (var sProp in currentObj.GetType().GetProperties(bindingFlags))
            {
                var tProp = result.GetType().GetProperty(sProp.Name, bindingFlags);

                if (tProp != null)
                {
                    if (sProp.Name.Equals(tProp.Name))
                    {
                        var sValue = sProp.GetValue(currentObj);

                        if (!sProp.PropertyType.IsComplex() && !tProp.PropertyType.IsComplex())
                        {
                            tProp.SetValue(result, sValue);
                        }
                    }
                }
            }

            return result;
        }

        public static bool IsComplex(this Type type)
        {
            if (type.IsSubclassOf(typeof(System.ValueType)) || type.Equals(typeof(string)))
                return false;
            else
                return true;
        }
    }
}
