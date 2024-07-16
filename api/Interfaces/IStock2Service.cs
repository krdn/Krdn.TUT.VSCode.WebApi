using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces;

public interface IStock2Service
{
    Task<List<Stock>> GetAllAsync();
}
