using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumer03
{
    class Program
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
            //DirectExchangeConsumer.Consume(channel);
            //TopicExchangeConsumer.Consume(channel);
            //HeaderExchangeConsumer.Consume(channel);
            FanoutExchangeConsumer.Consume(channel);
        }
    }
}