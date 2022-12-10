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
    public class HomeController : Controller
    {
        string Baseurl = "http://localhost:56341/api/anuncio/";
        public async Task<ActionResult> Index()
        {
            List<AnuncioModel> manifiestosList = new List<AnuncioModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync("");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    manifiestosList = JsonConvert.DeserializeObject<List<AnuncioModel>>(EmpResponse);

                }
                return View(manifiestosList);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            AnuncioModel manifiestosList = new AnuncioModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + id);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync("");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    manifiestosList = JsonConvert.DeserializeObject<AnuncioModel>(EmpResponse);
                }
                return View(manifiestosList);
            }
        }
        string Baseurl2 = "http://localhost:56341/api/postulacion/";
        public async Task<ActionResult> CreatePostulacion(string referencias, string dui, int? idAnuncio)
        {
            try
            {
                PostulacionModel EmpInfo = new PostulacionModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl2);
                    client.DefaultRequestHeaders.Clear();
                    PostulacionModel postulacion = new PostulacionModel();
                    UsuariosModel user = (UsuariosModel)System.Web.HttpContext.Current.Session["User"];
                    postulacion.idUsuario = 1;
                    postulacion.referencias = "TEst";
                    postulacion.DUI = "asdasd";
                    postulacion.solvenciaPNC = 1;
                    postulacion.antecedentes = 1;
                    postulacion.idAnuncio = 1;
                    postulacion.estado = 1;
                    postulacion.active = 1;
                    var myContent = JsonConvert.SerializeObject(postulacion);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = await client.PostAsync(Baseurl2, byteContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        return Content("1");
                    }
                    return Content("0");
                }

            }
            catch (Exception ex)
            {
                return Content("Error de aplicativo" + ex.Message);
            }

        }


    }
}