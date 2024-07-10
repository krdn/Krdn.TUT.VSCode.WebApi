using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    // Auto Mapper를 사용하여 DTO와 Entity간의 매핑을 수행 할수 있지만 
    // 직접 매핑을 수행하는 클래스를 생성하여 사용할수 있습니다.
    public static class StockMappers
    {
        /// <summary>
        /// 확장 메서드를 사용하여 Stock Entity를 StockDto로 변환합니다.
        /// this Stock stockModel에서 this 키워드는 Stock 타입의 인스턴스에 대해 확장 메서드를 정의하고 있음을 의미합니다. 즉, Stock 타입의 어떤 객체에서도 이 확장 메서드를 호출할 수 있게 됩니다. 예를 들어, ToStockDto라는 확장 메서드가 있다면, Stock 타입의 객체 stock에 대해 stock.ToStockDto()와 같이 메서드를 호출할 수 있습니다.
        /// </summary>
        /// <param name="stockModel">his Stock stockModel에서 this 키워드는 Stock 타입의 인스턴스에 대해 확장 메서드를 정의하고 있음</param>
        /// <returns></returns>
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }

        // 확장 메서드를 사용하여 CreateStockRequestDto를 Stock Entity로 변환합니다.
        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}