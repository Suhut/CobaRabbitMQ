using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQConsumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            var exchangeName = "demo-header-exchange";
            var routeKeyName = string.Empty;
            var queueName = "demo-header-queue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Headers);
            channel.QueueDeclare(queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

            var header = new Dictionary<string, object> { { "account", "new" } };


            channel.QueueBind(queueName, exchangeName, routeKeyName, header);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}