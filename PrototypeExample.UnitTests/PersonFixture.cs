using PrototypeExample.ConsoleApp;
using PrototypeExample.ConsoleApp.People;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrototypeExample.UnitTests
{
    public class PersonFixture : IDisposable
    {
        public int OriginalId => 666;
        public int NewId => 999;        
        public Person Person1 { get; private set; }
        public Person Person2 { get; private set; }
        public Person PersonShallowClone { get; private set; }
        public Person PersonDeepClone { get; private set; }
        public Person PersonThread1 { get; private set; }
        public Person PersonThread2 { get; private set; }

        public PersonFixture()
        {
            Person1 = Person.GetInstance();
            Person2 = Person.GetInstance();
            SetupValuesperson1();
            ShallowCopy();
            DeepCopy();

            this.SetupInThread().Wait();
            Person1.IdInfo.IdNumber = NewId;
        }

        private void DeepCopy() => PersonDeepClone = Person1.DeepCopy();

        private void ShallowCopy()
        {
            PersonShallowClone = Person1.ShallowCopy();
            PersonShallowClone.Name = "Johny Walker";
        }

        private async Task SetupInThread()
        {
            var task1 = Task.Run(() => {
                PersonThread1 = Person.GetInstanceThreadSafe();
                PersonThread1.Name = "Kenny";
            });
        
            var task2 = Task.Run(() => {
                PersonThread2 = Person.GetInstanceThreadSafe();
                PersonThread2.Name = "Stan";
            });

            await task1;
            await task2;        
        }

        private void SetupValuesperson1()
        {
            Person1.Age = 42;
            Person1.BirthDate = Convert.ToDateTime("1977-01-01");
            Person1.Name = "Jack Daniels";
            Person1.IdInfo = new IdInfo(OriginalId);
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PrototypeFixture()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
