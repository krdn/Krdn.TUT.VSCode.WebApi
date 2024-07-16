using api.Data;
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
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.Include(s => s.Comments).ToListAsync(); // comment 필요함.
    }
}
