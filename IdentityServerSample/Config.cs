﻿using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerSample
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
               new ApiResource("api","my Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={"api"}
                },
                new Client()
                {
                    ClientId="pwdClient",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={"api"},
                    RequireClientSecret=false//不用输入secret
        }
    };
}

public static List<TestUser> GetTestUsers()
{
    return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="xcz",
                    Password="123456"
                }
            };
}
    }
}
