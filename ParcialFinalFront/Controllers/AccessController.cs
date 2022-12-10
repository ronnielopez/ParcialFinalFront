using Newtonsoft.Json;
using ParcialFinalFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParcialFinalFront.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        string Baseurl = "http://localhost:56341/api/login/";
        public async Task<ActionResult> Login(string user, string password)
        {
            try
            {
                UsuariosModel EmpInfo = new UsuariosModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    UsuariosModel login = new UsuariosModel();
                    login.email = user;
                    login.pwd = password;
                    var myContent = JsonConvert.SerializeObject(login);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = await client.PostAsync(Baseurl, byteContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        EmpInfo = JsonConvert.DeserializeObject<UsuariosModel>(EmpResponse);
                        Session["user"] = EmpInfo;
                    }
                    return Content((EmpInfo.tipoUsuario).ToString());
                }

            }
            catch (Exception ex)
            {
                return Content("Error de aplicativo" + ex.Message);
            }

        }


    }
}