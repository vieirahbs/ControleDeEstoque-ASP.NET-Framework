using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
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
            List<GrupoProdutoModel> retorno = new List<GrupoProdutoModel>();

            using (SqlConnection conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from TB_GRUPO_PRODUTO order by GP_NOME";
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        retorno.Add(new GrupoProdutoModel
                        {   //O reader retorna um object, por isso é necessário fazer a conversão
                            Id = (int)reader["ID"],
                            Nome = (string)reader["GP_NOME"],
                            Ativo = (bool)reader["GP_ATIVO"],
                        }); ;
                    }
                }
            }
            return retorno;
        }

        public static GrupoProdutoModel RecuperarGrupoProduto(int id)
        {
            GrupoProdutoModel retorno = null;

            using (SqlConnection conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from TB_GRUPO_PRODUTO where ID = @ID";

                    comando.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        retorno = new GrupoProdutoModel
                        {
                            Id = (int)reader["ID"],
                            Nome = (string)reader["GP_NOME"],
                            Ativo = (bool)reader["GP_ATIVO"],
                        };
                    }
                }
            }
            return retorno;
        }

        public static bool DeleteGrupoProduto(int id)
        {
            bool retorno = false;

            if (RecuperarGrupoProduto(id) != null)
            {
                using (SqlConnection conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (SqlCommand comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from TB_GRUPO_PRODUTO where ID = @ID";
                        comando.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;

                        retorno = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }
            return retorno;
        }

        public int CreateUpdateGrupoProduto()
        {
            int retorno = 0;
            GrupoProdutoModel grupoProduto = RecuperarGrupoProduto(this.Id);
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (grupoProduto == null)
                    {
                        comando.CommandText = "insert into TB_GRUPO_PRODUTO values (@GP_NOME, @GP_ATIVO);" +
                            "select convert(int, scope_identity())";
                        comando.Parameters.Add("@GP_NOME", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@GP_ATIVO", SqlDbType.Bit).Value = this.Ativo ? 1 : 0;
                        
                        retorno = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update TB_GRUPO_PRODUTO set GP_NOME = @Nome, GP_ATIVO = @Ativo " +
                            "where ID = @ID";
                        comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@Ativo", SqlDbType.Bit).Value = this.Ativo ? 1 : 0;
                        comando.Parameters.Add("@ID", SqlDbType.Int).Value = this.Id;

                        //Executa e retorna um inteiro
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            retorno = this.Id;
                        }
                    }
                }
            }
            return retorno;
        }
    }
}
    