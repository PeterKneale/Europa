using System;
using Europa.Infrastructure;
using FluentValidation;

namespace Europa.Feed.Messages
{
    public class RetrievePodcastCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
    }

    public class RetrievePodcastValidator : MessageValidator<RetrievePodcastCommand>
    {
        public RetrievePodcastValidator()
        {
            RuleFor(x => x.Link).NotEmpty();
        }
    }


}
