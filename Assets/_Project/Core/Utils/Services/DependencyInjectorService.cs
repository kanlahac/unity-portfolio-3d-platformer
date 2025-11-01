namespace Project.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class DependencyInjectorService
    {
        public static void AutoInject(object target, DependencyProvider provider)
        {
            Type controllerType = target.GetType();
            Type providerType = provider.GetType();

            FieldInfo[] fields = controllerType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(
                    item =>
                        item.IsDefined(typeof(InjectFieldAttribute), true)
                ).ToArray();

            foreach (FieldInfo field in fields)
            {
                string fieldName = field.Name.TrimStart('_');

                PropertyInfo dependencyProperty = providerType.GetProperty(
                    fieldName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase
                );

                if (dependencyProperty != null && dependencyProperty.PropertyType.IsAssignableFrom(dependencyProperty.PropertyType))
                {
                    object dependencyValue = dependencyProperty.GetValue(provider);

                    field.SetValue(target, dependencyValue);
                }
            }
        }
    }   
}