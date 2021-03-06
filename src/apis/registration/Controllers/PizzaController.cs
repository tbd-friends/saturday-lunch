using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using registration.Commands;

namespace registration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PizzaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task AddNewPizza(Pizza pizza)
        {
            await _mediator.Send(new AddPizza
            {
                Name = pizza.Name,
                Price = pizza.Price,
                Ingredients = from i in pizza.Ingredients
                              select new AddPizza.Ingredient
                              {
                                  Name = i.Name,
                                  Quantity = i.Quantity
                              }
            });
        }
    }

    public class Pizza
    {
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