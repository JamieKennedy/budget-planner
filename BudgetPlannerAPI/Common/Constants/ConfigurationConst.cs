namespace Common.Constants
{
    public static class ConfigurationConst
    {
        public static class Cors
        {
            public const string POLICY_SECTION = "CorsPolicy";
            public const string POLICY_NAME = "Name";
            public const string ALLOWED_ORIGINS = "AllowedOrigins";
            public const string ALLOWED_METHODS = "AllowedMethods";
            public const string ALLOWED_HEADERS = "AllowedHeaders";
            public const string ALLOW_CREDENTIALS = "AllowCredentials";

            public const string ALLOW_ALL = "*";
        }

        public static class JWT
        {
            public const string SECTION = "JwtSettings";
            public const string SECRET = "Secret";
            public const string VALID_ISSUER = "ValidIssuer";
            public const string VALID_AUDIENCE = "ValidAudience";
        }

        public static class Sql
        {
            public const string USERNAME = "SQLServer:username";
            public const string PASSWORD = "SQLServer:password";
        }

    }
}
