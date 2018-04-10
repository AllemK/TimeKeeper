using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace TimeKeeper.OAuth
{
    public class InMemoryManager
    {
        public IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName="Read Employee Data",
                    IncludeAllClaimsForUser=true
                }
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
                        {
                new Client
                {
                    ClientId = "timekeeper",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("$ch00l".Sha256())
                    },
                    ClientName = "TimeKeeper",
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        "read"
                    },
                    Enabled = true
                }
            };
        }
    }
}