using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class ProjectsController : BaseController
    {
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Projects.Get()
                .Where(x => x.Name.Contains(h.filter))
                .Header(h)
                .ToList()
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all projects", "INFO");
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Project project = TimeKeeperUnit.Projects.Get(id);
            if (project == null)
            {
                Logger.Log($"No such project with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned project with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(project));
            }
        }

        /// <summary>
        /// Insert new Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Project project)
        {
            try
            {
                TimeKeeperUnit.Projects.Insert(project);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log("Inserted new project", "INFO");
                    return Ok(project);
                }
                else
                {
                    throw new Exception("Failed inserting new project, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Project
        /// </summary>
        /// <param name="project"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Project project, int id)
        {
            try
            {
                if (TimeKeeperUnit.Projects.Get(id) == null)
                {
                    Logger.Log($"No such project with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Projects.Update(project, id);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log($"Updated project with id {id}", "INFO");
                    return Ok(project);
                }
                else
                {
                    throw new Exception($"Failed updating project with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Project project = TimeKeeperUnit.Projects.Get(id);
                if (project == null)
                {
                    Logger.Log($"No such project with id {id}");
                    return NotFound();
                }

                /*Tried to delete all of the foreign key contraint items
                 * within the delete function, however it requires more
                 * attetion, and debugging, for now left alone until
                 * more consultation needed
                DetailsController dc = new DetailsController();
                foreach (var item in TimeKeeperUnit.Details.Get().Where(x => x.Project.Id == project.Id)) {
                    dc.Delete(item.Id);
                }
                */

                TimeKeeperUnit.Projects.Delete(project);
                TimeKeeperUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
