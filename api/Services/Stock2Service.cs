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

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _stock2Repository.GetAllAsync();
    }
}
