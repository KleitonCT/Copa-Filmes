using CopaFilmes.Business;
using CopaFilmes.Domain;
using CopaFilmes.Presentation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CopaFilmes.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FilmeViewModel vm = new FilmeViewModel
            {
                Filmes = new FilmeBusiness().GetFilmes()
            };
            Session["Filmes"] = vm.Filmes;
            return View(vm);
        }

        [HttpGet]
        public ActionResult ResultadoFinal(string ids)
        {
            FilmeViewModel vm = new FilmeViewModel();
            vm.Filmes.Add(((List<Filme>)Session["Filmes"]).FirstOrDefault(x => x.Id == ids.Split(',')[0]));
            vm.Filmes.Add(((List<Filme>)Session["Filmes"]).FirstOrDefault(x => x.Id == ids.Split(',')[1]));
            return View("Resultado", vm);
        }

        [HttpPost]
        public ActionResult Resultado(string[] ids)
        {
            List<Filme> filmes = (List<Filme>)Session["Filmes"];
            FilmeViewModel vm = new FilmeViewModel
            {
                Filmes = new FilmeBusiness().IniciarCampeonato(filmes.Where(x => ids.Contains(x.Id)).ToList())
            };
            return Json(vm.Filmes);
        }
    }
}