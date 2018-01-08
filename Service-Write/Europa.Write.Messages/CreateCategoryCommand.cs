using System;
using Europa.Infrastructure;
using FluentValidation;

namespace Europa.Write.Messages
{
    public class CreateCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateCategoryValidator : MessageValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
