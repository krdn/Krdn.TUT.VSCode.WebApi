window.onload = function() {
  const ui = SwaggerUIBundle({
      url: "/swagger/v1/swagger.json",
      dom_id: '#swagger-ui',
      presets: [
          SwaggerUIBundle.presets.apis,
          SwaggerUIStandalonePreset
      ],
      layout: "StandaloneLayout",
      onComplete: async function() {
          async function fetchToken() {
              try {
                  const response = await fetch('/api/token/generate'); // JWT 발급 API 엔드포인트
                  alert();
                  const data = await response.json();
                  return data.token;
              } catch (error) {
                  console.error('Error fetching JWT token:', error);
                  return null;
              }
          }

          const token = await fetchToken();
          if (token) {
              ui.preauthorizeApiKey('Bearer', token);
          }
      }
  });
};
