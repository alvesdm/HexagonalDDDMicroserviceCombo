namespace Infrastructure.Configuration
{
    public class Constants
    {
        public class Configuration
        {
            public class Service
            {
                public static string Name => "Service:Name";
                public static string Version => "Service:Version";
            }

            public class Broker
            {
                public static string Host => "Broker:Host";
                public static string Password => "Broker:Password";
                public static string User => "Broker:User";
                public static string Port => "Broker:Port";
                public static string VirtualHost => "Broker:VirtualHost";
                public static string Queues => "Broker:Queues";
            }

            public class Api
            {
                public static string BaseUri => "Api:BaseUri";
                public static string PingBackUri => "Api:PingBackUri";
            }
        }
    }
}