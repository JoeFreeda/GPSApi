using GPSApi.DBAccess;
using GPSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GPSApi.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/GPS/save")]
        //[Route("api/GPS/Update")]
        public HttpResponseMessage Save(PlaceModel Model)
        {
            if (ModelState.IsValid)
            {
                int PlaceId = DBContext.SaveDetails(Model);
                DBContext.SaveSubPlaceDetails(Model, PlaceId);
                return Request.CreateResponse(HttpStatusCode.OK, "Request Saved...");
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Validation Failed");
        }

        [HttpGet]
        [Route("api/GPS/GetAll")]
        public HttpResponseMessage GetAll()
        {
            if (ModelState.IsValid)
            {
                var ObjPlace = DBContext.GetAll();
                DBContext.GetSubPlace(ObjPlace);
                return Request.CreateResponse(HttpStatusCode.OK, ObjPlace);
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Validation Failed");
        }


        [HttpGet]
        [Route("api/GPS/GetByPlace")]
        public HttpResponseMessage GetAll(string Place)
        {
            if (ModelState.IsValid)
            {
                var ObjPlace = DBContext.GetByPlace(Place);
                DBContext.GetSubPlace(ObjPlace);
                return Request.CreateResponse(HttpStatusCode.OK, ObjPlace);
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Validation Failed");
        }
    }
}
