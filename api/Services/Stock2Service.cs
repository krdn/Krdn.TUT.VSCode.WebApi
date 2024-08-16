using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services;

public class Stock2Service : IStock2Service
{
    private readonly IStock2Repository _stock2Repository;

    public Stock2Service(IStock2Repository stock2Repository)
    {
        _stock2Repository = stock2Repository;
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        return await _stock2Repository.CreateAsync(stockModel);
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        return await _stock2Repository.DeleteAsync(id);
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _stock2Repository.GetAllAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _stock2Repository.GetByIdAsync(id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto)
    {
        return await _stock2Repository.UpdateAsync(id, updateStockDto);
    }
}
