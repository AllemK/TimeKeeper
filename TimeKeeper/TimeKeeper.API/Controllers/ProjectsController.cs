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
            var list = TimeUnit.Projects.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Project project = TimeUnit.Projects.Get(id);
            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeFactory.Create(project));
            }
        }

        public IHttpActionResult Post([FromBody] Project project)
        {
            try
            {
                TimeUnit.Projects.Insert(project);
                TimeUnit.Save();
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
                if (TimeUnit.Projects.Get(id) == null) return NotFound();
                TimeUnit.Projects.Update(project, id);
                TimeUnit.Save();
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
                Project project = TimeUnit.Projects.Get(id);
                if (project == null) return NotFound();
                TimeUnit.Projects.Delete(project);
                TimeUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
