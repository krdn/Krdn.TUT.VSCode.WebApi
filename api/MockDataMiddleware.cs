using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MockDataMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JObject _swaggerDoc;

    public MockDataMiddleware(RequestDelegate next)
    {
        _next = next;

        // swagger.json 파일 로드
        var swaggerJson = File.ReadAllText("swagger.json");
        _swaggerDoc = JObject.Parse(swaggerJson);
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value.TrimEnd('/');
        var method = context.Request.Method.ToLower();

        // OpenAPI paths에서 경로와 메서드 매칭
        var pathItem = _swaggerDoc["paths"]
            .Children<JProperty>()
            .FirstOrDefault(p => string.Equals(p.Name.TrimEnd('/'), path, StringComparison.OrdinalIgnoreCase));

        if (pathItem != null)
        {
            var operation = pathItem.Value[method];

            if (operation != null)
            {
                // 응답 스키마 가져오기
                var responses = operation["responses"];
                var response200 = responses["200"] ?? responses["default"];

                if (response200 != null)
                {
                    var content = response200["content"];
                    var applicationJson = content?["application/json"];

                    if (applicationJson != null)
                    {
                        var schema = applicationJson["schema"];

                        if (schema != null)
                        {
                            // 스키마를 기반으로 Mock 데이터 생성
                            var mockData = GenerateMockDataFromSchema(schema);
                            var mockJson = JsonConvert.SerializeObject(mockData);
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(mockJson);
                            return;
                        }
                    }
                }
            }
        }

        // 매칭되지 않으면 다음 미들웨어로
        await _next(context);
    }

    private object GenerateMockDataFromSchema(JToken schema)
    {
        if (schema["type"] != null)
        {
            var type = schema["type"].ToString();

            switch (type)
            {
                case "object":
                    var obj = new JObject();
                    var properties = schema["properties"] as JObject;

                    if (properties != null)
                    {
                        foreach (var prop in properties)
                        {
                            obj[prop.Key] = JToken.FromObject(GenerateMockDataFromSchema(prop.Value));
                        }
                    }

                    return obj;
                case "array":
                    var itemSchema = schema["items"];
                    var array = new JArray();

                    // 예시로 3개의 아이템 생성
                    for (int i = 0; i < 3; i++)
                    {
                        array.Add(JToken.FromObject(GenerateMockDataFromSchema(itemSchema)));
                    }

                    return array;
                case "string":
                    return new Faker().Random.Word();
                case "integer":
                    return new Faker().Random.Int();
                case "number":
                    return new Faker().Random.Double();
                case "boolean":
                    return new Faker().Random.Bool();
                default:
                    return null;
            }
        }
        else if (schema["$ref"] != null)
        {
            // 레퍼런스 처리
            var reference = schema["$ref"].ToString();
            var definitionName = reference.Split('/').Last();
            var definitions = _swaggerDoc["components"]?["schemas"];
            var definition = definitions?[definitionName];

            if (definition != null)
            {
                return GenerateMockDataFromSchema(definition);
            }
        }

        return null;
    }
}
