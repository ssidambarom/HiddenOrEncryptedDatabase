using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IntialProject.DbContexts;

namespace IntialProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            MyDbContext context,
            ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = _context.Customers.FirstOrDefault(_ => _.Id == id) ?? default(Customer);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post(PostCustomerRequest request)
        {
            if (request is null) return BadRequest("Request cannot be null");
            _context.Add(request);
            _context.SaveChanges();
            return Created(Url.RouteUrl(ControllerContext), request);
        }
    }
}