using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Handlers;
using Newtonsoft.Json;
using PSDUas.Models;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace PSDUas.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class HabitController : ApiController
    {
        private HabitDatabaseEntities1 habitDatabaseEntities = new HabitDatabaseEntities1();

        //no 1
        [HttpGet]
        [Route("{user_id}/habits")]
        //[Route("api/v1/users/{userID}/habits")]

        public HttpResponseMessage findUserId(Guid user_id)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject
                    (habitDatabaseEntities.Habit.Where(p => p.user_id == user_id)));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //no 2
        [HttpGet]
        [Route("{user_id}/{id}")]
        public HttpResponseMessage findIdAndUserId(Guid user_id, Guid id)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject
                    (habitDatabaseEntities.Habit.Where(p => p.user_id == user_id).Where(p => p.Id == id)));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //no 3
        [HttpPost]
        [Route("{user_id}/habits")]
        public HttpResponseMessage create(Guid user_id, [FromBody] Habit habit)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                var h = Repo.habitRepo.habitRegister(habit.name, habit.days_off, user_id);
                if(h == null)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed,"Error (Name must be 2-100 words)");
                }
                return result;
            }
            
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //no 4
        [HttpPut]
        [Route("{user_id}/habits/{id}")]
        public HttpResponseMessage update(Guid user_id, Guid id, [FromBody] Habit habit)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                var updateHabit = habitDatabaseEntities.Habit.Single(p => p.Id == id);
                if (updateHabit.user_id == user_id)
                {
                    updateHabit.name = habit.name;
                    updateHabit.days_off = habit.days_off;

                    habitDatabaseEntities.SaveChanges();
                }

                return result;
            }
            catch
            {
               return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //no 5
        [HttpDelete]
        [Route("{user_id}/habits/{Id}")]
        //[Route("api/v1/users/{userID}/habits")]

        public HttpResponseMessage delete(Guid user_id, Guid id) 
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                var habit = habitDatabaseEntities.Habit.Find(id);
                if(habit.user_id == user_id)
                {
                    habitDatabaseEntities.Habit.Remove(habit);
                    habitDatabaseEntities.SaveChanges();
                }

                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // no 6
        [HttpPost]
        [Route("{user_id}/habits/{id}/logs")]

        public HttpResponseMessage postHabit(Guid user_id, Guid id)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var habit = habitDatabaseEntities.Habit.Find(id);
                if (habit.user_id == user_id)
                {
                    var j = Repo.habitRepo.habitRegister(habit.name, habit.days_off, user_id);
                }
                var h = new Habit();
                h.current_streak++;
                if (h.current_streak == 5)
                {
                   // habitDatabaseEntities.Badges.Add();
                }

                h.log_count++;
                h.logs = String.Concat(h.logs, ", " + DateTime.Now.ToString());

                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            
        }
    }
}
