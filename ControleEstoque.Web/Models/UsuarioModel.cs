using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUsuario(string login, string senha)
        {
            bool ret = false;
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = "Data Source=DESKTOP-QINDOBS;Initial Catalog=CONTROLE_ESTOQUE;" +
                    "Integrated Security=True;Connect Timeout=30";
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = $"select count(*) from TB_USUARIO where " +
                        $"US_LOGIN = '{login}' and US_SENHA = '{senha}'";

                    ret = ((int)comando.ExecuteScalar() > 0);
                }
            }
            return ret;
        }
    }
}