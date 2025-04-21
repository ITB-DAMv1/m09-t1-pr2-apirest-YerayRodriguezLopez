using System.IdentityModel.Tokens.Jwt;

namespace T1PR2_Client.Tools
{
    public static class TokenHelper
    {
        public static bool IsTokenSession(string token)
        {
            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        public static bool IsTokenExpired(string token)
        {
            if (token.StartsWith("{") && token.Contains("\"token\":"))
            {
                var json = System.Text.Json.JsonDocument.Parse(token);
                token = json.RootElement.GetProperty("token").GetString();
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var expiration = jwt.ValidTo;
            return expiration < DateTime.UtcNow;
        }

        public static System.Security.Claims.ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
        
                var identity = new System.Security.Claims.ClaimsIdentity(jwt.Claims, "jwt");
                return new System.Security.Claims.ClaimsPrincipal(identity);
            }
            catch
            {
                return null;
            }
        }
    }
}
