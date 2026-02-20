namespace BadgeCatalog.Domain.Security;

public sealed class JwkKey
{
    //JWK = JSON Web Key
    public string Kty { get; } //tipo da chave (RSA)
    public string Kid { get; } //id da chave
    public string Use { get; } // uso (signature)
    public string Alg { get; } //alg → algoritmo
    public string N { get; } //modulus RSA
    public string E { get; } //exponent

    public JwkKey(string kty, string kid, string use, string alg, string n, string e)
    {
        Kty = kty;
        Kid = kid;
        Use = use;
        Alg = alg;
        N = n;
        E = e;
    }
}