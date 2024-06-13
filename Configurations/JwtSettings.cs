namespace asp_net_ecommerce_api.Configurations
{
    public class JwtSettings
    {
        public string? Key { get; set; } 
        //= "secret_key";
        public string? Issuer { get; set; } 
        //= "https://localhost";
        public string? Audience { get; set; } 
        //= "https://localhost";
        public int ExpiresInMinutes { get; set; }
        // = 120;
    }
}
