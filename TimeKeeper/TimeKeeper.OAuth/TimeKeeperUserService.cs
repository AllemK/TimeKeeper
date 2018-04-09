using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.OAuth
{
    public class TimeKeeperUserService:UserServiceBase
    {
        private readonly UnitOfWork unitOfWork;
        public TimeKeeperUserService(UnitOfWork unit)
        {
            unitOfWork = unit;
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = unitOfWork.Employees.Get(x => x.Email == context.UserName && x.Password == context.Password).FirstOrDefault();
            if (user == null) context.AuthenticateResult = new AuthenticateResult("Bad username or password");
            else context.AuthenticateResult = new AuthenticateResult(context.UserName, context.Password);
            return base.AuthenticateLocalAsync(context);
        }
    }
}