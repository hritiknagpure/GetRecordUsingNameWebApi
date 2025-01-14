using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TurfBookingSystem.Models;

namespace TurfBookingSystem.Controllers
{
    [Route("api/Currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ApplicationDbContext _turfBooking;

        public CurrencyController(ApplicationDbContext applicationDbContext)
        {
            _turfBooking = applicationDbContext;
        }

        // GET: api/Currency

        [HttpGet]

        public IActionResult GetAllCurrencies()

        {

            var currencies = _turfBooking.Currencies.ToList();

            return Ok(currencies);

        }



        // GET: api/Currency/{id}

        [HttpGet("{id:int}")]

        public IActionResult GetCurrencyById(int id)

        {

            var currency = _turfBooking.Currencies.FirstOrDefault(c => c.currencyId == id);

            if (currency == null)

            {

                return NotFound($"Currency with ID {id} not found.");

            }

            return Ok(currency);

        }// GET: api/Currency/GetByName/{Mypropertyname}

        [HttpGet("GetByName/{Mypropertyname}")]

        public IActionResult GetCurrencyByName(string Mypropertyname)

        {
            var currency = _turfBooking.Currencies

              .FirstOrDefault(c => c.Mypropertyname.ToLower() == Mypropertyname.ToLower());

            if (currency == null)

            {

                return NotFound($"Currency with name '{Mypropertyname}' not found.");

            }

            return Ok(currency);

        }


        // PUT: api/Currency/ByName/{Mypropertyname}
        [HttpPut("ByName/{Mypropertyname}")]
        public IActionResult UpdateCurrencyByName(string Mypropertyname, [FromBody] Currency updatedCurrency)
        {
            if (string.IsNullOrEmpty(Mypropertyname))
            {
                return BadRequest("Currency name cannot be empty.");
            }

            var existingCurrency = _turfBooking.Currencies
                .FirstOrDefault(c => c.Mypropertyname.ToLower() == Mypropertyname.ToLower());

            if (existingCurrency == null)
            {
                return NotFound($"Currency with name '{Mypropertyname}' not found.");
            }

            // Update the properties of the existing currency with the new values
            existingCurrency.Mypropertyname = updatedCurrency.Mypropertyname;
         

            _turfBooking.SaveChanges();

            return Ok($"Currency with name '{Mypropertyname}' has been updated successfully.");
        }



        // POST: api/Currency

        [HttpPost]

        public IActionResult CreateCurrency([FromBody] Currency currency)

        {

            if (currency == null)

            {

                return BadRequest("Currency object is null.");

            }
            _turfBooking.Currencies.Add(currency);

            _turfBooking.SaveChanges();

            return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.currencyId }, currency);

        }

        // PUT: api/Currency/{id}

        [HttpPut("{id}")]

        public IActionResult UpdateCurrency(int id, [FromBody] Currency updatedCurrency)

        {

            var existingCurrency = _turfBooking.Currencies.FirstOrDefault(c => c.currencyId == id);



            if (existingCurrency == null)

            {

                return NotFound($"Currency with ID {id} not found.");

            }
            //existingCurrency.MyProperty = updatedCurrency.MyProperty;

            existingCurrency.Mypropertyname = updatedCurrency.Mypropertyname;

            _turfBooking.SaveChanges();

            return NoContent();

        }

        // ... (Existing methods: GetAllCurrencies, GetCurrencyById, GetCurrencyByName, CreateCurrency, UpdateCurrency)

        // DELETE: api/Currency/ByName/{Mypropertyname}
        [HttpDelete("ByName/{Mypropertyname}")]
        public IActionResult DeleteCurrencyByName(string Mypropertyname)
        {
            if (string.IsNullOrEmpty(Mypropertyname))
            {
                return BadRequest("Currency name cannot be empty.");
            }

            var currency = _turfBooking.Currencies
                .FirstOrDefault(c => c.Mypropertyname.ToLower() == Mypropertyname.ToLower());

            if (currency == null)
            {
                return NotFound($"Currency with name '{Mypropertyname}' not found.");
            }

            _turfBooking.Currencies.Remove(currency);
            _turfBooking.SaveChanges();

            return Ok($"Currency with name '{Mypropertyname}' has been deleted.");
        }

        // DELETE: api/Currency/{id} (Existing Delete method - improved)
        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            var currency = _turfBooking.Currencies.Find(id); // Use Find for efficiency

            if (currency == null)
            {
                return NotFound($"Currency with ID {id} not found.");
            }

            _turfBooking.Currencies.Remove(currency);
            _turfBooking.SaveChanges();

            return Ok($"Currency with ID {id} has been deleted.");
        }
    }
}
