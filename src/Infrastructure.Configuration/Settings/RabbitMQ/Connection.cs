using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration.Settings.RabbitMQ
{
    public class Connection
    {
        public Connection(IConfiguration configuration)
        {
            VirtualHost = configuration.GetSection(Constants.Configuration.Broker.VirtualHost).Value;
            Host = configuration.GetSection(Constants.Configuration.Broker.Host).Value;
            Port = int.Parse(configuration.GetSection(Constants.Configuration.Broker.Port).Value);
            User = configuration.GetSection(Constants.Configuration.Broker.User).Value;
            Password = configuration.GetSection(Constants.Configuration.Broker.Password).Value;
        }

        public string VirtualHost { get; set; }

        public string Password { get; set; }

        public string User { get; set; }

        public int Port { get; set; }

        public string Host { get; set; }
    }
}