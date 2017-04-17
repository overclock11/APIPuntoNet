using fabrisetas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        // GET: api/user/lista
        // lista todos los usuarios
        [ActionName("lista")]
        public string Get()
        {
            String result = "";

            fabrisetas = new fabricetasEntities();
            var usuarios = (from user in fabrisetas.user
                                   select new { userid = user.USER_ID, user.TIPO, user.NAME, user.IDENTIFICATION_NUMBER, user.FIRST_NAME, user.LAST_NAME }).ToList();
            result = JsonConvert.SerializeObject(usuarios);

            return result;
        }

        // GET: api/user/tipo/ARTISTA
        [ActionName("lista")]
        public string Get(string tipoUsuario)
        {
            string result = "";
            string tipo = tipoUsuario.ToUpper();
            fabrisetas = new fabricetasEntities();
            var usuario = (from us in fabrisetas.user
                           where us.TIPO == tipo
                           select new { us.USER_ID, us.TIPO, us.NAME, us.IDENTIFICATION_NUMBER, us.FIRST_NAME, us.LAST_NAME });
            result = JsonConvert.SerializeObject(usuario);
            return result;
        }


        // GET: api/user/unico/5
        [ActionName("unico")]
        public string Get(int id)
        {
            String result = "";
            fabrisetas = new fabricetasEntities();
            var usuario = (from us in fabrisetas.user
                           where us.USER_ID  == id
                           select new { us.USER_ID, us.TIPO , us.NAME , us.IDENTIFICATION_NUMBER, us.FIRST_NAME , us.LAST_NAME });

            result = JsonConvert.SerializeObject(usuario);
            

            return result;
        }

        // POST: api/user/actualizar
        [ActionName("crear")]
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

        // PUT: api/user/actualizar
        [ActionName("actualizar")]
        public HttpResponseMessage Put(int id, [FromBody]user value)
        {
            fabrisetas = new fabricetasEntities();
            user nuevoUsuario = new user();

            user usuario = (from us in fabrisetas.user
                           where us.USER_ID == id
                           select us).FirstOrDefault();
            usuario.NAME = value.NAME;
            fabrisetas.SaveChanges();

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            string respuesta = JsonConvert.SerializeObject(usuario);
            response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");

            return response;
        }


        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
        }
    }
}
