using System.Collections.Generic;

namespace Console.ApplicationCore.Configurations
{
    public class TokenConfig
    {
        public string Secret { get; set; }
        public List<string> Audiences { get; set; } = new();
        public List<string> Issuers { get; set; } = new();
        public string Audience => string.Join(";", Audiences);
        public string Issuer => string.Join(";", Issuers);
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateLifetime { get; set; }
        public int AccessTokenExpireInSecond { get; set; }
        public int RefreshTokenExpireInSecond { get; set; }
    }
}
