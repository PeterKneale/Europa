using System;
using Europa.Infrastructure;
using FluentValidation;

namespace Europa.Write.Messages
{
    public class CreatePodcastCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class CreatePodcastValidator : MessageValidator<CreatePodcastCommand>
    {
        public CreatePodcastValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Link).NotEmpty();
        }
    }
}

