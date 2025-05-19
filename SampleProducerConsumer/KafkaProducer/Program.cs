
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    static async Task Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9094" };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var random = new Random();
        var stocks = new List<string> { "AAPL", "MSFT", "GOOG", "TSLA", "AMZN" };

        for (int i = 0; i < 1000; i++)
        {
            var stock = stocks[random.Next(stocks.Count)];
            var price = Math.Round(100 + random.NextDouble() * 900, 2);
            var message = new
            {
                Symbol = stock,
                Price = price,
                Timestamp = DateTime.UtcNow.ToString("o")
            };

            var value = JsonSerializer.Serialize(message);
            var result = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value = value });
            Console.WriteLine($"Produced to {result.TopicPartitionOffset}: {value}");
            await Task.Delay(500);
        }
    }
}
