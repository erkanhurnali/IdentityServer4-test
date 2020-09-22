using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Kimlik
{
    public class Config
    {
        // public static IEnumerable<IdentityResource> IdentityResources =>
        // new IdentityResource[]
        // {
        //     new IdentityResources.OpenId(),
        //     new IdentityResources.Profile(),
        // };
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1.okuma"),
                new ApiScope("api1.yazma"),
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "1 Nolu API")
                {
                    Scopes = {"api1.okuma"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "istemci1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("cokgizli".Sha256())
                    },
                    AllowedScopes = { "api1.okuma" }
                },

                new Client
                {
                    ClientId = "istemci2",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("budacokgizli".Sha256())
                    },
                    AllowedScopes = { "api1.okuma", "api1.yazma" }
                },

                new Client
                {
                    ClientId = "istemci3",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequireClientSecret = false,
                    AllowedScopes = { "api1.okuma", "api1.yazma" },
                    Claims = new[]
                    {
                        new ClientClaim("yetki", "okuma")
                    }
                },
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "user",
                    Password = "user",
                    Claims = new[]
                    {
                        new Claim("yetki", "okuma")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "admin",
                    Claims = new[]
                    {
                        new Claim("yetki", "güncelleme")
                    }
                }
            };
        }

    }
}