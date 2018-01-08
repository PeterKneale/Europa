using Europa.Write.Messages;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class SetPodcastValidatorTests
    {
        private readonly CreatePodcastValidator _validator;

        public SetPodcastValidatorTests()
        {
            _validator = new CreatePodcastValidator();
        }

        [Fact]
        public void Should_have_error_when_id_is_empty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Should_have_error_when_category_id_is_empty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CategoryId, Guid.Empty);
        }

        [Fact]
        public void Should_have_error_when_link_is_null()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Link, null as string);
        }
    }
}

