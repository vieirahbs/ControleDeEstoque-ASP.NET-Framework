using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class CadastroController : Controller
    {
        public static List<GrupoProdutoModel> _listaGrupoProduto = new List<GrupoProdutoModel>()
        {
            new GrupoProdutoModel() { Id=1, Nome="Livros", Ativo=true },
            new GrupoProdutoModel() { Id=2, Nome="Mouses", Ativo=true },
            new GrupoProdutoModel() { Id=3, Nome="Monitores", Ativo=false }
        };

        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(_listaGrupoProduto);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RecuperaGrupoProduto(int id)
        {
            return Json(_listaGrupoProduto.Find(x => x.Id == id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AdicionaGrupoProduto(GrupoProdutoModel grupo)
        {
            GrupoProdutoModel registroDB = _listaGrupoProduto.Find(x => x.Id == grupo.Id);
            if (registroDB == null)
            {
                registroDB = grupo;
                registroDB.Id = _listaGrupoProduto.Max(x => x.Id) + 1;
                _listaGrupoProduto.Add(registroDB);
            }
            else
            {
                registroDB.Nome = grupo.Nome;
                registroDB.Ativo = grupo.Ativo;
            }
            return Json(registroDB);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirGrupoProduto(int id)
        {
            bool ret = false;

            GrupoProdutoModel registroBD = _listaGrupoProduto.Find(x => x.Id == id);

            if (registroBD != null)
            {
                _listaGrupoProduto.Remove(registroBD);
                ret = true;
            }

            return Json(ret);
        }

        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }

        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }

        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

        [Authorize]
        public ActionResult Usuario()
        {
            return View();
        }
    }
}