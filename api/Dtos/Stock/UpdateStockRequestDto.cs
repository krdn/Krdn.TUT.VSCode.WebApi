using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol은 최대 10 문자 이하여야 합니다.")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(10, ErrorMessage = "Company Name 최대 10 문자 이하여야 합니다.")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 1000000000, ErrorMessage = "Purchase는 0 이상 1000000000 이하여야 합니다.")]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100, ErrorMessage = "Current는 0 이상 1000000000 이하여야 합니다.")]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Sector은 최대 10 문자 이하여야 합니다.")]
    public string Industry { get; set; } = string.Empty;
    [Range(1, 5000000000, ErrorMessage = "MarketCap은 0 이상 5000000000 이하여야 합니다.")]
    public long MarketCap { get; set; }
}
