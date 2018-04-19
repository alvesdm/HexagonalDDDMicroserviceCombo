namespace Infrastructure.Configuration
{
    public class Constants
    {
        public class Configuration
        {
            public static string ServiceName => "Service:Name";

            public class Broker
            {
                public static string Host => "Broker:Host";
                public static string Password => "Broker:Password";
                public static string User => "Broker:User";
                public static string Port => "Broker:Port";
                public static string VirtualHost => "Broker:VirtualHost";
                public static string Queues => "Broker:Queues";
            }
        }
    }
}