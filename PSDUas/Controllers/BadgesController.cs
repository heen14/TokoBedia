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

namespace PSDUas.Controllers
{
    [RoutePrefix("api/v2/users")]
    public class BadgesController : ApiController
    {
        private HabitDatabaseEntities1 badgeDatabaseEntities = new HabitDatabaseEntities1();
        
        //no 7
        [HttpGet]
        [Route("{user_id}/badges")]
        public HttpResponseMessage findUserBadge(Guid user_id)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(badgeDatabaseEntities.Badges.ToList()));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
