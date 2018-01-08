using Europa.Write.Messages;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class DeletePodcastValidatorTests
    {
        private readonly DeletePodcastValidator _validator;

        public DeletePodcastValidatorTests()
        {
            _validator = new DeletePodcastValidator();
        }

        [Fact]
        public void Should_have_error_when_id_is_empty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}

