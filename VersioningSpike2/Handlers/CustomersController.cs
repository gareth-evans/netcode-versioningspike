using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VersioningSpike2.Contracts;

namespace VersioningSpike2.Controllers
{
    public class CustomersController
    {
        [HttpGet]
        [VersionedRoute("api/customers/{id}")]
        [Produces(typeof(CustomerV1))]
        public IActionResult Get(string id)
        {
            var customer = new CustomerV1 {FirstName = "Gareth"};

            return new OkObjectResult(customer);
        }

        [HttpGet]
        [VersionedRoute("api/customers/{id}")]
        [Produces(typeof(CustomerV2))]
        public IActionResult GetV2(string id)
        {
            var customer = new CustomerV2 { Forename = "Gareth" };

            return new OkObjectResult(customer);
        }

        [VersionedRoute("api/customers")]
        public IActionResult Create([FromBody] CustomerV1 customer)
        {
            return new OkObjectResult(customer);
        }

        [VersionedRoute("api/customers")]
        public IActionResult CreateV2([FromBody] CustomerV2 customer)
        {
            return new OkObjectResult(customer);
        }
    }
}