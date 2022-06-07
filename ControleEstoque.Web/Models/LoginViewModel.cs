using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Senha { get; set; }
        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }



    }
}