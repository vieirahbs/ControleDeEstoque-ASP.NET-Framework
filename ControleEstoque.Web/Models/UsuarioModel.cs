using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ControleEstoque.Web.Helpers;

namespace ControleEstoque.Web.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUsuario(string login, string senha)
        {
            bool ret = false;
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select count(*) from TB_USUARIO where US_LOGIN=@US_LOGIN and US_SENHA=@US_SENHA";
                    
                    comando.Parameters.Add("@US_LOGIN", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@US_SENHA", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);

                    ret = ((int)comando.ExecuteScalar() > 0);
                }
            }
            return ret;
        }
    }
}