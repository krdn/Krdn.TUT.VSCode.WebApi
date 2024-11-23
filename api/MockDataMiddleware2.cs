using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MockDataMiddleware2
{
    private readonly RequestDelegate _next;
    private readonly JObject _swaggerDoc;

    public MockDataMiddleware2(RequestDelegate next)
    {
        _next = next;

        // Load the swagger.json file
        var swaggerJson = File.ReadAllText("swagger.json");
        _swaggerDoc = JObject.Parse(swaggerJson);
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value.TrimEnd('/');
        var method = context.Request.Method.ToLower();

        // Match the path and method in the OpenAPI paths
        var matchingPath = _swaggerDoc["paths"]
            .Children<JProperty>()
            .FirstOrDefault(p => IsPathMatching(p.Name, path));

        if (matchingPath != null)
        {
            var operation = matchingPath.Value[method];

            if (operation != null)
            {
                // Get the response schema
                var responses = operation["responses"];
                var response200 = responses["200"] ?? responses.First as JProperty;

                if (response200 != null)
                {
                    var content = response200["content"];
                    var applicationJson = content?["application/json"];

                    if (applicationJson != null)
                    {
                        var schema = applicationJson["schema"];

                        if (schema != null)
                        {
                            // Generate mock data based on the schema
                            var mockData = GenerateMockDataFromSchema(schema);
                            var mockJson = JsonConvert.SerializeObject(mockData, Formatting.Indented);
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(mockJson);
                            return;
                        }
                    }
                }
            }
        }

        // If no match, continue to the next middleware
        await _next(context);
    }

    private bool IsPathMatching(string templatePath, string requestPath)
    {
        var templateSegments = templatePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var requestSegments = requestPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (templateSegments.Length != requestSegments.Length)
            return false;

        for (int i = 0; i < templateSegments.Length; i++)
        {
            if (templateSegments[i].StartsWith("{") && templateSegments[i].EndsWith("}"))
                continue; // Path parameter, so it matches

            if (!string.Equals(templateSegments[i], requestSegments[i], StringComparison.OrdinalIgnoreCase))
                return false;
        }

        return true;
    }

    private object GenerateMockDataFromSchema(JToken schema)
    {
        if (schema["type"] != null)
        {
            var type = schema["type"].ToString();
            var format = schema["format"]?.ToString();

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

                    // Generate a random number of items (e.g., between 1 and 5)
                    var faker = new Faker();
                    int itemCount = faker.Random.Int(1, 5);

                    for (int i = 0; i < itemCount; i++)
                    {
                        array.Add(JToken.FromObject(GenerateMockDataFromSchema(itemSchema)));
                    }

                    return array;
                case "string":
                    return GenerateStringData(format);
                case "integer":
                    return new Faker().Random.Int(1, 1000);
                case "number":
                    return new Faker().Random.Double(0, 1000);
                case "boolean":
                    return new Faker().Random.Bool();
                default:
                    return null;
            }
        }
        else if (schema["$ref"] != null)
        {
            // Handle references
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

    private object GenerateStringData(string format)
    {
        var faker = new Faker();

        switch (format)
        {
            case "date-time":
                return faker.Date.Recent().ToString("o");
            case "email":
                return faker.Internet.Email();
            case "uri":
            case "url":
                // Generate image URL using placeholder.com
                int width = faker.Random.Int(100, 500);
                int height = faker.Random.Int(100, 500);
                string backgroundColor = faker.Random.Hexadecimal(6, string.Empty).Replace("0x", "");
                string textColor = faker.Random.Hexadecimal(6, string.Empty).Replace("0x", "");
                string text = faker.Commerce.ProductName();
                string encodedText = Uri.EscapeDataString(text);

                return $"https://via.placeholder.com/{width}x{height}/{backgroundColor}/{textColor}?text={encodedText}";
            case "uuid":
                return Guid.NewGuid().ToString();
            default:
                // Default to random string
                return faker.Lorem.Word();
        }
    }
}
