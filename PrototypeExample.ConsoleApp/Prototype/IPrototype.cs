using System;

namespace PrototypeExample.ConsoleApp.Prototype
{
    public interface IPrototype<T> : ICloneable where T : class
    {
        T ShallowCopy();
        T DeepCopy();
    }
}
