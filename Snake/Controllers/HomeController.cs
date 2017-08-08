using System;
using System.Net;
using System.Web.Mvc;

namespace Snake.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pontos()
        {
            return PartialView("_Pontos");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult VerificarLogin(string nome, string senha)
        {
            var rp = new Repository.Repository();
            if (rp.VerificarUsuario(nome) == senha)
            {
                Response.Cookies["usuario"].Value = nome;
                Response.Cookies["usuario"].Expires = DateTime.Now.AddDays(1);
                return View("Index");
            }
            else
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Content("Senha ou usuário invalido(s)!");
            }
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Cadastrar(string nome, string senha)
        {
            var rp = new Repository.Repository();
            if (rp.ValidarNovoUsuario(nome))
            {
                rp.NovoUsuario(nome, senha);
                return View("Login");
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CancelarCadastro()
        {
            return View("Login");
        }

        public ActionResult ExibirTabela()
        { 
            var rp = new Repository.Repository();
            return View(rp.RetornaTabela());
        }

        public void SalvarPontos(int pontos)
        {
            var usuario = Request.Cookies["usuario"].Value;
            var rp = new Repository.Repository();
            rp.SalvarPontos(usuario, pontos);
        }
    }
}

