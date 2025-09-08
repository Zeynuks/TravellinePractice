using CurrencyExchanger.Entities;
using CurrencyExchanger.Models.Currency;
using CurrencyExchanger.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchanger.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class CurrencyController : ControllerBase
    {
        private ICurrencyService _currencyService;

        public CurrencyController( ICurrencyService currencyService )
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Currency>? users = _currencyService.GetAll();
            return Ok( users );
        }

        [HttpGet]
        [Route( "{code}" )]
        public IActionResult GetByCode( string code )
        {
            Currency? user = _currencyService.GetByCode( code );
            return Ok( user );
        }

        [HttpGet]
        [Route( "/prices" )]
        public IActionResult GetPriceChanges( [FromQuery] GetPricesRequest model )
        {
            IEnumerable<PriceChange>? result = _currencyService.GetPriceChanges( model );
            return Ok( result );
        }
    }
}