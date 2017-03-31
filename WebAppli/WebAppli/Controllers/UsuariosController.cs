using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAppli.datos;

namespace WebAppli.Controllers
{

    public class UsuariosController : ApiController
    {
        rutaEntities mod;
        // GET api/values
        public string Get()
        {
            string result = "";

            mod = new rutaEntities();
            List<usuario> lista = (from usuarios in mod.usuario
                                   select usuarios).ToList();

            result = JsonConvert.SerializeObject(lista);
            return result;

        }

        // GET api/values/5
        public string Get(int id)
        {
            string result = "";

            mod = new rutaEntities();
            usuario ser = mod.usuario.First(usuarios => usuarios.usuario_id == id);

            result = JsonConvert.SerializeObject(ser);
            return result;
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]usuario value)
        {
            mod = new rutaEntities();

            usuario nuevoUsuario = new usuario();

            nuevoUsuario.usuario_nombre = value.usuario_nombre;
            nuevoUsuario.usuario_login = value.usuario_login;
            nuevoUsuario.usuario_pass = Base64Encode(value.usuario_pass);
            nuevoUsuario.usuario_avatar = value.usuario_avatar;
            mod.usuario.Add(nuevoUsuario);
            int x = mod.SaveChanges();                        

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            string jsonRespuesta = JsonConvert.SerializeObject(nuevoUsuario);
            response.Content = new StringContent(jsonRespuesta,Encoding.UTF8,"application/json");

            return response;


        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]usuario value)
        {
            mod = new rutaEntities();
            usuario ser = mod.usuario.First(usuarios => usuarios.usuario_id == id);
            ser.usuario_login = value.usuario_login;
            ser.usuario_nombre = value.usuario_nombre;
            ser.usuario_pass = Base64Encode(value.usuario_pass);
            ser.usuario_pass = value.usuario_avatar;
            int x = mod.SaveChanges();

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            string jsonRespuesta = JsonConvert.SerializeObject(ser);
            response.Content = new StringContent(jsonRespuesta, Encoding.UTF8, "application/json");

            return response;

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            mod = new rutaEntities();
            usuario ser = mod.usuario.First(usuarios => usuarios.usuario_id == id);
            mod.usuario.Remove(ser);
            int x = mod.SaveChanges();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
