using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.OAuth
{
    public class InMemoryManager
    {
        private readonly UnitOfWork _unit = new UnitOfWork();
        public List<InMemoryUser> GetUsers()
        {
            List<InMemoryUser> users = new List<InMemoryUser>();
            var employees = _unit.Employees.Get();
            foreach(var employee in employees)
            {
                users.Add(new InMemoryUser
                {
                    Subject = employee.Email,
                    Username = employee.Email,
                    Password = employee.Password,
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, employee.FullName),
                    }
                });
            }
            return users;
        }

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