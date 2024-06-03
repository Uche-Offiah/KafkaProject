using Confluent.Kafka;
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092", // Kafka broker address
            ClientId = "dotnet-producer"
        };

        using var producer = new ProducerBuilder<string, string>(config).Build();

        Console.WriteLine("Enter messages to send (type 'exit' to quit):");

        string topic = "test-topic"; // Kafka topic name

        while (true)
        {
            string message = Console.ReadLine();
            if (message.ToLower() == "exit")
                break;

            var deliveryReport = producer.ProduceAsync(topic, new Message<string, string> { Key = null, Value = message }).Result;

            Console.WriteLine($"Message '{message}' sent to partition {deliveryReport.Partition}, offset {deliveryReport.Offset}");
        }
    }
}
