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
/// https://velog.io/@yarogono/ASP.NET-Core-Service-Repository-%ED%8C%A8%ED%84%B4-%EC%A0%81%EC%9A%A9%ED%95%B4%EC%84%9C-Controller-%EC%BD%94%EB%93%9C-%EB%B6%84%EB%A6%AC%ED%95%98%EA%B8%B0
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stocks = await _stock2Service.GetAllAsync();
        var stockDtos = stocks.Select(stock => stock.ToStockDto());

        return Ok(stockDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stockModel = await _stock2Service.GetByIdAsync(id);
        if (stockModel is null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stockModel = stockDto.ToStockFromCreateDto();
        await _stock2Service.CreateAsync(stockModel);

        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stockModel = await _stock2Service.UpdateAsync(id, updateStockDto);
        if (stockModel is null)
        {
            return NotFound();
        }

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stockModel = await _stock2Service.DeleteAsync(id);
        if (stockModel is null)
        {
            return NotFound();
        }

        return Ok(stockModel.ToStockDto());
    }




}
