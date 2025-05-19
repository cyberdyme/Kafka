
using Confluent.Kafka;
using System;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9094",
            GroupId = "csharp-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("test-topic");

        Console.WriteLine("Consuming messages from 'test-topic'...");

        try
        {
            while (true)
            {
                var consumeResult = consumer.Consume();
                var json = consumeResult.Value;

                try
                {
                    var message = JsonSerializer.Deserialize<StockMessage>(json);
                    Console.WriteLine($"[{message.Timestamp}] {message.Symbol}: ${message.Price}");
                }
                catch (JsonException)
                {
                    Console.WriteLine("Received non-JSON or invalid message: " + json);
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }

    class StockMessage
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public string Timestamp { get; set; }
    }
}
