namespace DevFreela.Infra.Auth;

public class JwtOptions
{
    public string Audience { get; set; }

    public string Issuer { get; set; }

    public string Key { get; set; }
}