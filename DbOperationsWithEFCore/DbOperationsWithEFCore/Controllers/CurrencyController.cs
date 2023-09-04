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

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
		{
			var result = await _appDbContext.Currencies.FindAsync(id);
			return Ok(result);
		}

		//[HttpGet("{name}/{description}")]
		[HttpGet("{name}")]
		public async Task<IActionResult> GetCurrencyByNameAsync([FromRoute] string name, [FromQuery] string? description)
		{
			//var result = await _appDbContext.Currencies
			//	.FirstOrDefaultAsync(x =>
			//	x.Title == name
			//	&& (string.IsNullOrEmpty(description) || x.Description == description));

			var result = await _appDbContext.Currencies
				.Where(x =>
				x.Title == name
				&& (string.IsNullOrEmpty(description) || x.Description == description)).ToListAsync();
			return Ok(result);
		}

		[HttpPost("all")]
		public async Task<IActionResult> GetCurrenciesByIdsAsync([FromBody] List<int> ids)
		{
			//var ids = new List<int> { 1, 6, 2, 3 };
			var result = await _appDbContext.Currencies
				.Where(x => ids.Contains(x.Id))
				.ToListAsync();
			return Ok(result);
		}

	}
}
