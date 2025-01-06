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
        }

        // GET: api/Currency/GetByName/{Mypropertyname} fectch datarecord using name keyword
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

        // DELETE: api/Currency/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            Currency foundCurrency = null;

            // Iterate through the list to find the currency
            foreach (var currency in _turfBooking.Currencies)
            {
                if (currency.currencyId == id)
                {
                    foundCurrency = currency;
                    break;
                }
            }

            if (foundCurrency == null)
            {
                return NotFound($"Currency with ID {id} not found.");
            }

            // Remove the currency
            _turfBooking.Currencies.Remove(foundCurrency);

            return Ok($"Currency with ID {id} has been deleted.");
        }

    }
}
