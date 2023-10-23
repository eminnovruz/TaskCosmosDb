namespace TaskApiCosmos.Models
{
    public class JWTConfig
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpireMunites { get; set; }
    }
}
