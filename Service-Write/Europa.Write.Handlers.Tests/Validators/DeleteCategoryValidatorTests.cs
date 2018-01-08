using Europa.Write.Messages;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class DeleteCategoryValidatorTests
    {
        private readonly DeleteCategoryValidator _validator;

        public DeleteCategoryValidatorTests()
        {
            _validator = new DeleteCategoryValidator();
        }

        [Fact]
        public void Should_have_error_when_id_is_empty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}

