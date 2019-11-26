using PrototypeExample.ConsoleApp.Prototype;
using System;

namespace PrototypeExample.ConsoleApp.People
{
    public class Person : Prototype<Person>
    {
        #region Variables

        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        #endregion

        #region Singleton
        
        private static Person instance;

        private static readonly object _lock = new object();
        public static Person GetInstance() => instance ??= new Person();
        public static Person GetInstanceThreadSafe()
        {
            if(instance == null)
                lock (_lock)
                    return GetInstance();

            return instance;
        }

        private Person() { }

        #endregion

        protected override void PreserveAttributes(Person clone)
        {
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = Name;
        }
    }

}
