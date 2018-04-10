using System.Web.Http;
using TimeKeeper.API.Models;
using TimeKeeper.API.Reports;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Controllers
{
    public class BaseController : ApiController
    {
        UnitOfWork unit;
        ModelFactory factory;
        ReportFactory reports;

        public UnitOfWork TimeKeeperUnit
        {
            get
            {
                if (unit == null) unit = new UnitOfWork();
                return unit;
            }
        }

        public ModelFactory TimeKeeperFactory
        {
            get
            {
                if (factory == null) factory = new ModelFactory();
                return factory;
            }
        }

        public ReportFactory TimeKeeperReports
        {
            get
            {
                if (reports == null) reports = new ReportFactory(TimeKeeperUnit);
                return reports;
            }
        }
    }
}
