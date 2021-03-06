using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using MediatR;
using registration.Commands.Handlers.Models;

namespace registration.Commands.Handlers
{
    public class AddPizzaHandler : IRequestHandler<AddPizza>
    {
        public async Task<Unit> Handle(AddPizza request, CancellationToken cancellationToken)
        {
            using var producer = new ProducerBuilder<Guid, Pizza>(new Dictionary<string, string>()
                {
                    {"bootstrap.servers", "localhost:9092"}
                })
                .SetKeySerializer(new KeySerializer())
                .SetValueSerializer(new ValueSerializer<Pizza>())
                .Build();

            var pizzaUid = Guid.NewGuid();

            await producer.ProduceAsync("pizzas", new Message<Guid, Pizza>()
            {
                Key = pizzaUid,
                Value = new Pizza
                {
                    UId = pizzaUid,
                    Name = request.Name,
                    Price = request.Price,
                    Ingredients = from i in request.Ingredients
                                  select new Ingredient
                                  {
                                      Name = i.Name,
                                      Quantity = i.Quantity
                                  }
                }
            }, cancellationToken);

            return Unit.Value;
        }
    }

    public class KeySerializer : ISerializer<Guid>
    {
        public byte[] Serialize(Guid data, SerializationContext context)
        {
            return data.ToByteArray();
        }
    }

    public class ValueSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
}