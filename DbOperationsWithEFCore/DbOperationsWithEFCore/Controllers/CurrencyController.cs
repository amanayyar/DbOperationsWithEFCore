using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Controllers
{
	[Route("api/currencies")]
	[ApiController]
	public class CurrencyController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;

		public CurrencyController(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		[HttpGet("")]
		public IActionResult GetAllCurrencies()
		{
			//var result = _appDbContext.Currencies.ToList();
			var result = (from currencies in _appDbContext.Currencies
						 select currencies).ToList();
			return Ok(result);
		}
		[HttpGet("GetAllCurrenciesAsync")]
		public async Task<IActionResult> GetAllCurrenciesAsync()
		{
			//var result = await _appDbContext.Currencies.ToListAsync();
			var result = await (from currencies in _appDbContext.Currencies
						  select currencies).ToListAsync();
			return Ok(result);
		}

	}
}
