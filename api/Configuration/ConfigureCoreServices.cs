using api.Interfaces;
using api.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Configuration;

public static class ConfigureCoreServices
{
    /// <summary>
    /// 코드 베이스를 깔끔하게 유지하기 위해 서비스를 추가하는 확장 메서드를 사용합니다.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IStock2Service, Stock2Service>();

        return services;
    }
}
