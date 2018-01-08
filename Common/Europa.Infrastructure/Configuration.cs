
namespace Europa.Infrastructure
{
    public interface IConfiguration
    {
        string Host { get; }
        string Database { get; }
        string UserName { get; }
        string Password { get; }
        int Port { get; }
        string ConnectionString { get; }
    }

    public class Configuration : IConfiguration
    {
        public string ConnectionString => $"host={Host};port={Port};database={Database};username={UserName};password={Password}";

        public virtual string Host => "HOST";

        public virtual string Database => "europa";

        public virtual string UserName => "postgres";

        public virtual string Password => "password";

        public virtual int Port => 32776;
    }
}
