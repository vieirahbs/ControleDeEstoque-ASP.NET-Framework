using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ControleEstoque.Web.Models;

namespace ControleEstoque.Web.Controllers
{
    public class ContaController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) //Direciona para a página de login
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {//Se os dados digitados pelo usuário ainda estiverem invalidos
             //apresenta novamente a página de login:
                return View(login);
            }

            bool achou = (UsuarioModel.ValidarUsuario(login.Usuario, login.Senha));
            if (achou)
            {
                //Salva os dados nos cookies
                FormsAuthentication.SetAuthCookie(login.Usuario, login.LembrarMe);
                if (Url.IsLocalUrl(returnUrl))//Se a url for da aplicação, após logado
                {//irá direcionar para ela.
                    return Redirect(returnUrl);
                }
                else
                {//Se não, vai para a Home.
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login inválido.");
            }

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logout()//Método de Logout
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



    }
}