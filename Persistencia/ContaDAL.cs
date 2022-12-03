using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistencia
{
    public class ContaDAL
    {
        private SqlConnection conn;
        private CategoriaDAL categoria;
        public ContaDAL(SqlConnection conn)
        {
            this.conn = conn;
            string strConn = Db.Conexao.GetStringConnection();
            this.categoria = new CategoriaDAL(new SqlConnection(strConn));
        }
        public List<Conta> ListarTodos()
        {
            List<Conta> contas = new List<Conta>();
            var command = new SqlCommand("select con.id, con.descricao , con.valor, con.tipo , con.data_vencimento, cat.nome , cat.id as categoria_id from contas con inner join categorias cat on con.categoria_id = cat.id ",this.conn);
            this.conn.Open();

            using (SqlDataReader rd = command.ExecuteReader())
            {
                while (rd.Read())
                {
                    Conta conta = new Conta()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Descricao = rd["descricao"].ToString(),
                        Tipo = Convert.ToChar(rd["tipo"].ToString()),
                        Valor = Convert.ToDouble(rd["valor"].ToString())
                    };

                    int id_categoria = Convert.ToInt32(rd["id"].ToString());
                    conta.Categoria = this.categoria.GetCategoria(id_categoria);
                    contas.Add(conta);

                }
            }
            return contas;
        }

        //57:00
    }
}
