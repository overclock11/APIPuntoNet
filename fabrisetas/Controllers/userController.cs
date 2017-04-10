using fabrisetas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace fabrisetas.Controllers
{
    public class userController : ApiController
    {
        fabricetasEntities fabrisetas;
        // GET: api/Usuarios
        public string Get()
        {
            String result = "";

            fabrisetas = new fabricetasEntities();
            List<user> usuarios = (from user in fabrisetas.user
                                   select user).ToList();
            result = JsonConvert.SerializeObject(usuarios);

            return result;
        }

        // GET: api/Usuarios/5
        public string Get(int id)
        {
            String result = "";
            fabrisetas = new fabricetasEntities();
            user usuario = fabrisetas.user.First(usu => usu.USER_ID == id);

            result = JsonConvert.SerializeObject(usuario);

            return result;
        }

        // POST: api/Usuarios
        public HttpResponseMessage Post([FromBody]user value)
        {
            fabrisetas = new fabricetasEntities();
            user nuevoUsuario = new user();
            nuevoUsuario = value;
            try
            {
                fabrisetas.user.Add(nuevoUsuario);
                fabrisetas.SaveChanges();
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                string respuesta = JsonConvert.SerializeObject(nuevoUsuario);
                response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");
                return response;
            }
            catch
            {
                var response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        // PUT: api/Usuarios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
        }
    }
}
