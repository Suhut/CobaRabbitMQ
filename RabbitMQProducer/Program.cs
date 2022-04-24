using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQProducer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",

            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //DirectExchangePublisher.Publish(channel); //JIKA exchangeName && routeKeyName 
            //TopicExchangePublisher.Publish(channel); //JIKA exchangeName && routeKeyName 
            //HeaderExchangePublisher.Publish(channel);
            FanoutExchangePublisher.Publish(channel);

        }
    }
}