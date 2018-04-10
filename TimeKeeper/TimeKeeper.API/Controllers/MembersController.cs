using System;
using System.Linq;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.Utility;

namespace TimeKeeper.API.Controllers
{
    [TimeKeeperAuth(Roles: "Admin")]
    public class MembersController : BaseController
    {
        public IHttpActionResult Post(EngagementModel e)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new member, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Engagements.Insert(TimeKeeperFactory.Create(e, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log($"Inserted new member {e.Employee.Name}", "INFO");
                return Ok(new { e, memberId = TimeKeeperUnit.Engagements.Get().Count() });
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(EngagementModel e, int id)
        {
            try
            {
                if (TimeKeeperUnit.Engagements.Get(id) == null)
                {
                    Logger.Log($"No such member with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new member, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Engagements.Update(TimeKeeperFactory.Create(e, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated member {e.Employee.Name}", "INFO");
                return Ok(e);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var e = TimeKeeperUnit.Engagements.Get(id);
                if (e == null)
                {
                    Logger.Log($"No member found with id {id}");
                    return NotFound();
                }

                TimeKeeperUnit.Engagements.Delete(e);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted member with id {id}", "INFO");
                return Ok();
            }
            catch(Exception e)
            {
                Logger.Log(e.Message, "ERROR", e);
                return BadRequest(e.Message);
            }
        }
    }
}
