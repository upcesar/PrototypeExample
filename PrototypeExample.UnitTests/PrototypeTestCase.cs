using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PrototypeExample.UnitTests
{
    public class PrototypeTestCase : IClassFixture<PersonFixture>
    {
        private readonly PersonFixture fixture;

        public PrototypeTestCase(PersonFixture fixture) 
            => this.fixture = fixture;

        [Fact]
        public void PreviousValueChangedShallowCloneTest()
            => Assert.NotEqual("Jack Daniels", fixture.PersonShallowClone.Name);
        
        [Fact]
        public void FinalValueChangedShallowCloneTest()
            => Assert.Equal("Johny Walker", fixture.PersonShallowClone.Name);
        
        [Fact]
        public void ReferenceChangedShallowCloneTest()
            => Assert.Equal(fixture.NewId, fixture.PersonShallowClone.IdInfo.IdNumber);
        
        [Fact]
        public void ReferenceKeptDeepCloneTest()
            => Assert.Equal(fixture.OriginalId, fixture.PersonDeepClone.IdInfo.IdNumber);
    }
}
