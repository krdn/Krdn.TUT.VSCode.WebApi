using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repository;

public class Stock2Repository : IStock2Repository
{
    private readonly ApplicationDBContext _context;
    public Stock2Repository(ApplicationDBContext context)
    {
        _context = context;

    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync(); // stockModel에 Id가 부여됨.

        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        if (stockModel is null)
        {
            return null;
        }

        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.Include(s => s.Comments).ToListAsync(); // comment 필요함.
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto)
    {
        // id에 해당하는 Stock을 비동기로 찾는다.
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        if (existingStock == null)
        {
            return null;
        }

        existingStock.Symbol = updateStockDto.Symbol;
        existingStock.CompanyName = updateStockDto.CompanyName;
        existingStock.Purchase = updateStockDto.Purchase;
        existingStock.LastDiv = updateStockDto.LastDiv;
        existingStock.Industry = updateStockDto.Industry;
        existingStock.MarketCap = updateStockDto.MarketCap;

        await _context.SaveChangesAsync();
        return existingStock;
    }
}
