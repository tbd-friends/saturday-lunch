using System;
using System.Linq;
using graph.Schema;
using HotChocolate;
using MongoDB.Driver;

namespace graph
{
    public partial class Query
    {
        public IQueryable<Pizza> GetPizza([Service] IMongoDatabase database) =>
            database.GetCollection<Pizza>("pizzas").AsQueryable();
    }
}