using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace graph.Schema
{
    public class Pizza
    {
        [BsonId] public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

}