using api.Dtos.Stock;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces;

public interface IStock2Service
{
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> DeleteAsync(int id);
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto);
}
