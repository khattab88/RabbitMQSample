using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQSample.Models;
using System;
using System.Text;
using System.Text.Json;

Console.WriteLine("RabbitMQ Consumer");

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
channel.QueueDeclare(queue, durable: true, exclusive: false);

// consfigure consumer
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    string bodyString = Encoding.UTF8.GetString(body);

    var message = JsonSerializer.Deserialize<Booking>(bodyString);

    Console.WriteLine($"Message received: {message?.ToString()}");
};


channel.BasicConsume(queue, autoAck: true, consumer);

Console.ReadKey();