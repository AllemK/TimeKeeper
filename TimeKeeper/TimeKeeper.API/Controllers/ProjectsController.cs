﻿using System;
using System.Linq;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.Utility;

namespace TimeKeeper.API.Controllers
{
    [TimeKeeperAuth]
    public class ProjectsController : BaseController
    {
        [TimeKeeperAuth]
        public IHttpActionResult GetAll(string role, string teamId="")
        {
            if (role == "Admin")
            {
                var list = TimeKeeperUnit.Projects.Get().OrderBy(x => x.Name).ToList()
                            .Select(x => new { x.Id, x.Name })
                            .ToList();
                return Ok(list);
            }
            if (role.Contains("User") || role.Contains("Lead"))
            {
                var list = TimeKeeperUnit.Engagements.Get()
                    .Where(x => x.Team.Id == teamId)
                    .ToList()
                    .GroupBy(x=>x.Team.Projects)
                    .SelectMany(x=>x.Key)
                    .Select(y => new { y.Id, y.Name })
                    .ToList();
                return Ok(list);
            }
            return Ok();
        }

        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns></returns>
        [TimeKeeperAuth(Roles: "Admin")]
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Projects
                .Get(x => x.Name.Contains(h.filter))
                .AsQueryable()
                .Header(h)
                .ToList()
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all projects", "INFO");
            return Ok(list);
        }

        [TimeKeeperAuth(Roles: "Admin")]
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
        [TimeKeeperAuth(Roles: "Admin")]
        public IHttpActionResult Post([FromBody] ProjectModel project)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new project" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                project.StartDate = DateTime.Today;
                TimeKeeperUnit.Projects.Insert(TimeKeeperFactory.Create(project, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log("Inserted new project", "INFO");
                return Ok(project);
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
        [TimeKeeperAuth(Roles: "Admin")]
        public IHttpActionResult Put([FromBody] ProjectModel project, int id)
        {
            try
            {
                if (TimeKeeperUnit.Projects.Get(id) == null)
                {
                    Logger.Log($"No such project with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string message = $"Failed updating project with id {id}" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Projects.Update(TimeKeeperFactory.Create(project, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated project with id {id}", "INFO");
                return Ok(project);
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
        [TimeKeeperAuth(Roles: "Admin")]
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
