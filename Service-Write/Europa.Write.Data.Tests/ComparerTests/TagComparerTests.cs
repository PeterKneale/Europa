using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Europa.Write.Data.Tests.ComparerTests
{
    public class TagComparerTests
    {
        private readonly TagComparer _sut;

        public TagComparerTests()
        {
            _sut = new TagComparer();
        }

        [Fact]
        public void CheckNullsAreEqual()
        {
            _sut.Equals(null, null).Should().BeTrue();
            _sut.GetHashCode(null).Should().Be(_sut.GetHashCode(null));
        }

        [Fact]
        public void CheckNullObjectHasSameHashAs()
        {
            var x = new Tag();
            _sut.Equals(x, null).Should().BeFalse();
            _sut.GetHashCode(x).Should().Be(_sut.GetHashCode(null));
        }

        [Fact]
        public void ReferencesAreEqual()
        {
            var x = new Tag();
            var y = x;
            _sut.Equals(x, y).Should().BeTrue();
            _sut.GetHashCode(x).Should().Equals(_sut.GetHashCode(y));
        }

        [Fact]
        public void BothAreNotInitializedWithValues()
        {
            var x = new Tag();
            var y = new Tag();
            _sut.Equals(x, y).Should().BeTrue();
            _sut.GetHashCode(x).Should().Equals(_sut.GetHashCode(y));
        }

        [Fact]
        public void PropertiesAreBothEqual()
        {
            var id = Guid.NewGuid();
            var name = "test";
            var x = new Tag { Id = id, Name = name };
            var y = new Tag { Id = id, Name = name };
            _sut.Equals(x, y).Should().BeTrue();
            _sut.GetHashCode(x).Should().Equals(_sut.GetHashCode(y));
        }

        [Fact]
        public void PropertiesAreBothDifferent()
        {
            var x = new Tag { Id = Guid.NewGuid(), Name = "test1" };
            var y = new Tag { Id = Guid.NewGuid(), Name = "test2" };
            _sut.Equals(x, y).Should().BeFalse();
            _sut.GetHashCode(x).Should().NotBe(_sut.GetHashCode(y));
        }

        [Fact]
        public void IdsAreDifferent()
        {
            var x = new Tag { Id = Guid.NewGuid(), Name = "test1" };
            var y = new Tag { Id = Guid.NewGuid(), Name = "test2" };
            _sut.Equals(x, y).Should().BeFalse();
            _sut.GetHashCode(x).Should().NotBe(_sut.GetHashCode(y));
        }

        [Fact]
        public void NamesAreDifferent()
        {
            var id = Guid.NewGuid();
            var x = new Tag { Id = id, Name = "test1" };
            var y = new Tag { Id = id, Name = "test2" };
            _sut.Equals(x, y).Should().BeFalse();
            _sut.GetHashCode(x).Should().NotBe(_sut.GetHashCode(y));
        }
    }
}
