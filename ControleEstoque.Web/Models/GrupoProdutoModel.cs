using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public static List<GrupoProdutoModel> RecuperarLista()
        {
            var ret = new List<GrupoProdutoModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from TB_GRUPO_PRODUTO order by GP_NOME";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new Models.GrupoProdutoModel
                        {
                            Id = (int)reader["ID"],
                            Nome = (string)reader["GP_NOME"],
                            Ativo = (bool)reader["GP_ATIVO"],
                        }); ;
                    }
                }
            }
            return ret;
        }

        public static GrupoProdutoModel RecuperarGrupoProduto(int id)
        {
            GrupoProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = $"select * from TB_GRUPO_PRODUTO where ID = {id}";
                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["ID"],
                            Nome = (string)reader["GP_NOME"],
                            Ativo = (bool)reader["GP_ATIVO"],
                        };
                    }
                }
            }
            return ret;
        }

        public static bool DeleteGrupoProduto(int id)
        {
            bool ret = false;

            if (RecuperarGrupoProduto(id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = $"delete from TB_GRUPO_PRODUTO where ID = {id}";
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }
            return ret;
        }


        public int CreateGrupoProduto()
        {
            var ret = 0;
            var idGrupo = RecuperarGrupoProduto(this.Id);
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (idGrupo == null)
                    {
                        comando.CommandText = string.Format("insert into TB_GRUPO_PRODUTO " +
                            "values ('{0}', {1}); " +
                            "select convert(int, scope_identity())", this.Nome, this.Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {

                        comando.CommandText = string.Format(
                            "update TB_GRUPO_PRODUTO set GP_NOME = '{0}', GP_ATIVO = {1} where ID = {2}; ",
                            this.Nome, this.Ativo ? 1 : 0, this.Id);
                        //Executa e retorna um inteiro
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
    