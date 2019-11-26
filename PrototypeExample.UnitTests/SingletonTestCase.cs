using PrototypeExample.ConsoleApp;
using PrototypeExample.ConsoleApp.People;
using System;
using Xunit;

namespace PrototypeExample.UnitTests
{
    public class SingletonTestCase : IClassFixture<PersonFixture>
    {
        private readonly PersonFixture fixture;

        public SingletonTestCase(PersonFixture fixture) 
            => this.fixture = fixture;

        [Fact]
        public void SingletonInstanceTest() 
            => Assert.Same(fixture.Person1, fixture.Person2);
    
        [Fact]
        public void SingletonAsyncInstanceTest() 
            => Assert.Equal("Kenny", fixture.PersonThread2.Name);
    }
}
