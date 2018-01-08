using System;
using Europa.Infrastructure;
using FluentValidation;

namespace Europa.Write.Messages
{
    public class DeleteCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
    }

    public class DeleteCategoryValidator : MessageValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
