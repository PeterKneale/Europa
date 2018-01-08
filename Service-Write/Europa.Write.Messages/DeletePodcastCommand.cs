using System;
using Europa.Infrastructure;
using FluentValidation;

namespace Europa.Write.Messages
{
    public class DeletePodcastCommand : ICommand
    {
        public Guid Id { get; set; }
    }

    public class DeletePodcastValidator : MessageValidator<DeletePodcastCommand>
    {
        public DeletePodcastValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
