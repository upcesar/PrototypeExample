namespace PrototypeExample.ConsoleApp.Prototype
{
    public abstract class Prototype<T> : IPrototype<T> where T : class
    {
        public T ShallowCopy() => this.MemberwiseClone() as T;
        public T DeepCopy() => this.Clone() as T;
        public object Clone()
        {
            var clone = this.ShallowCopy();
            PreserveAttributes(clone);
            return clone;
        }
        protected abstract void PreserveAttributes(T clone);

    }
}
