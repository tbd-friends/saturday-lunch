using System;
using System.Collections.Generic;

namespace registration.Commands.Handlers.Models
{
    public class Pizza
    {
        public Guid UId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public double Price { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}