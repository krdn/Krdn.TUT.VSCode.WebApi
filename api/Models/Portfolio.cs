using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    /// <summary>
    /// 포트폴리오 모델
    /// AppUserStock 모델과 1:N 관계
    /// </summary>
    [Table("Portfolios")]
    public class Portfolio
    {
        public required string AppUserId { get; set; }
        public int StockId { get; set; }
        public required AppUser AppUser { get; set; }
        public required Stock Stock { get; set; }

    }
}
