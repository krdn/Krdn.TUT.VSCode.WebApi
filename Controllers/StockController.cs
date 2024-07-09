using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Krdn.TUT.VSCode.WebApi.Data;

using Microsoft.AspNetCore.Mvc;

namespace Krdn.TUT.VSCode.WebApi.Controllers
{
    [Route("api/[controller]")] // [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // stock list, stock details, stock add, stock update, stock delete

        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStock([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }



    }
}