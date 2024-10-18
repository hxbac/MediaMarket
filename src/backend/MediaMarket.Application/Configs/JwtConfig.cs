namespace MediaMarket.Application.Configs
{
    public class JwtConfig
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string SecretKey { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
