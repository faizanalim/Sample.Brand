using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Brand.Models;

namespace Sample.Brand.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private readonly BrandContext _dbContext;

		public BrandController(BrandContext dbContext)
		{
			_dbContext = dbContext;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Sample.Brand.Models.Brand>>> GetBrands()
		{
			if (_dbContext.Brands == null)
			{
				return NotFound();
			}
			return await _dbContext.Brands.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Sample.Brand.Models.Brand>> GetBrandId(int id)
		{
			if (_dbContext.Brands == null)
			{
				return NotFound();
			}
			var brand = await _dbContext.Brands.FindAsync(id);
			if (brand == null)
			{
				return NotFound();

			}
			return brand;
		}
		[HttpPost]
		public async Task<ActionResult<Sample.Brand.Models.Brand>> PostBrand(Sample.Brand.Models.Brand brand)
		{ 
		_dbContext.Brands.Add(brand);
		await _dbContext.SaveChangesAsync();
			return CreatedAtAction(nameof(GetBrands), new { id = brand.ID }, brand);
		
		}
	}
}
