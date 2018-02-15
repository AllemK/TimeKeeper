using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Repository;
using TimeKeeper.API.Models;

namespace TimeKeeper.API.Controllers
{
    public class BaseController : ApiController
    {
        UnitOfWork unit;
        ModelFactory factory;

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
    }
}
