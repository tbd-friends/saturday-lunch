using System.Collections.Generic;
using MediatR;

namespace registration.Commands
{
    public class AddPizza : IRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }

        public class Ingredient
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }
    }
}