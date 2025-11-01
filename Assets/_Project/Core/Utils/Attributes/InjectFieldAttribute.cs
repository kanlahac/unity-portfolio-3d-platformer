namespace Project.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectFieldAttribute : Attribute { }
}