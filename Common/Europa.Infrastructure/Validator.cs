using System;
using Autofac;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Europa.Infrastructure
{
    public interface IValidator
    {
        bool TryValidate<T>(T message, out ValidationResult result) where T : IMessage;
    }

    public class Validator : IValidator
    {
        private readonly IComponentContext _context;
        private readonly ILogger<Validator> _log;

        public Validator(IComponentContext context, ILogger<Validator> log)
        {
            _context = context;
            _log = log;
        }

        public bool TryValidate<T>(T message, out ValidationResult result) where T : IMessage
        {
            var name = message.GetType().Name;

            var validator = _context.ResolveOptional<IMessageValidator<T>>();
            if (validator == null)
            {
                // No validator found, warn and set an empty validation result.
                _log.LogWarning($"No validator for {name}");
                result = new ValidationResult();
                return true;
            }

            _log.LogInformation($"Validating {name}...");

            result = validator.Validate(new ValidationContext<T>(message));
            if (!result.IsValid)
            {
                _log.LogWarning($"Validation failed for {name}.");
                return false;
            }

            _log.LogInformation($"Validated {name}...");
            return true;
        }
    }
}
