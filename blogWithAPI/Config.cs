using Duende.IdentityServer.Models;

namespace blogWithAPI
{
    public static class Config 
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
          new IdentityResources.OpenId(),
          new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("blogapi", "Blog API")
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("blogapi", "Blog API")
            {
                Scopes = { "blogapi" }
            }
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "blogClient",
                ClientName = "Blog API Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "blogapi", "openid", "profile" }
            }
        };
    }
}
