using System;
using System.Linq;
using System.Reflection;

namespace Desafio.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static TTarget Copy<TTarget>(this object source, TTarget targetBase = default(TTarget), string excludeProperties = null)
        {
            if (source == null)
                return default(TTarget);

            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            TTarget target = targetBase != null ? targetBase : Activator.CreateInstance<TTarget>();

            string[] ignoreProperties = null;
            if (!string.IsNullOrWhiteSpace(excludeProperties))
                ignoreProperties = excludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                foreach (var propSource in source.GetType().GetProperties(bindingFlags))
                {
                    if (ignoreProperties != null)
                        if (ignoreProperties.Contains(propSource.Name))
                            continue;

                    var propTarget = target.GetType().GetProperty(propSource.Name, bindingFlags);

                    if (propTarget != null)
                    {
                        var sValue = propSource.GetValue(source);

                        if (!propSource.PropertyType.IsComplex() && !propTarget.PropertyType.IsComplex())
                            propTarget.SetValue(target, sValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return target;
        }

        public static bool IsComplex(this Type type)
        {
            if (type.IsSubclassOf(typeof(ValueType)) || type.Equals(typeof(string)))
                return false;
            else
                return true;
        }
    }
}
