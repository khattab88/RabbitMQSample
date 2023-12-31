﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var connectionfactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var connection = connectionfactory.CreateConnection();
            var channel = connection.CreateModel();

            string queue = "bookings";
            string routingKey = "bookings";
            channel.QueueDeclare(queue, durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", routingKey, body: body);
        }
    }
}
