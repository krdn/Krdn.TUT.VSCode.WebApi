using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Dtos.Stock;
using api.Mappers;

namespace api.Controllers;

/// <summary>
/// Service와 Repository를 사용하는 Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class Stock2Controller : ControllerBase
{
    private readonly IStock2Service _stock2Service;

    public Stock2Controller(IStock2Service stock2Service)
    {
        _stock2Service = stock2Service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stock2Service.GetAllAsync();
        var stockDtos = stocks.Select(stock => stock.ToStockDto());

        return Ok(stockDtos);
    }
}
