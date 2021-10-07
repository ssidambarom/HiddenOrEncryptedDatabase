using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkWithEncryptedDatabase.DbContexts;

namespace WorkWithEncryptedDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly EncryptedDbContext _context;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            EncryptedDbContext context,
            ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(_ => _.Id == id) ?? default(Customer);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCustomerRequest request)
        {
            if (request is null) return BadRequest("Request cannot be null");
            _context.Customers.Add(request);
            await _context.SaveChangesAsync();
            return Created(Url.RouteUrl(ControllerContext), request);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, PutCustomerRequest request)
        {
            if (request is null) return BadRequest("Request cannot be null");

            var customer = await _context.Customers.FirstOrDefaultAsync(_ => _.Id == id);
            if (customer == null) return NotFound();

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;

            await _context.SaveChangesAsync();

            return Ok(customer);
        }

    }
}