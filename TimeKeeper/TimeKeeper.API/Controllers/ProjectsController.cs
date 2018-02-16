using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class ProjectsController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Projects.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Project project = TimeKeeperUnit.Projects.Get(id);
            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeKeeperFactory.Create(project));
            }
        }

        public IHttpActionResult Post([FromBody] Project project)
        {
            try
            {
                TimeKeeperUnit.Projects.Insert(project);
                TimeKeeperUnit.Save();
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Project project, int id)
        {
            try
            {
                if (TimeKeeperUnit.Projects.Get(id) == null) return NotFound();
                TimeKeeperUnit.Projects.Update(project, id);
                TimeKeeperUnit.Save();
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Project project = TimeKeeperUnit.Projects.Get(id);
                if (project == null) return NotFound();
                TimeKeeperUnit.Projects.Delete(project);
                TimeKeeperUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
