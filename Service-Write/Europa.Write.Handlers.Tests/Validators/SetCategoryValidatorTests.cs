using Europa.Write.Messages;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Europa.Write.Handlers.Tests
{
    public class SetCategoryValidatorTests
    {
        private readonly CreateCategoryValidator _validator;

        public SetCategoryValidatorTests()
        {
            _validator = new CreateCategoryValidator();
        }

        [Fact]
        public void Should_have_error_when_id_is_empty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Should_have_error_when_name_is_null()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
        }
    }
}

