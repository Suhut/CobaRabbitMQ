
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQConsumer03
{
    public class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            var exchangeName = "demo-direct-exchange";
            var routeKeyName = "account.init";
            var queueName = "demo-direct-queue03";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

            channel.QueueBind(queueName, exchangeName, routeKeyName);
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