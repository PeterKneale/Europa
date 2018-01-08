using Europa.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Europa.Write.Data
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        IUnitOfWork Begin();
    }

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConfiguration _connection;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWorkFactory(IConfiguration connection, ILogger<UnitOfWork> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_connection, _logger);
        }

        public IUnitOfWork Begin()
        {
            var work = Create();
            work.Begin();
            return work;
        }
    }
}
