using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Persistencia
{
    public class CategoriaDAL
    {
        private SqlConnection conn;
        public CategoriaDAL(SqlConnection conn)
        {
            this.conn = conn;
        }
        public Categoria GetCategoria(int id)
        {
            Categoria categoria = new Categoria();
            var command = new SqlCommand($"select id, nome from categorias where id = @id", this.conn);
            command.Parameters.AddWithValue("@id", id);
            this.conn.Open();
            using (SqlDataReader rd = command.ExecuteReader())
            {
                rd.Read();
                categoria.Id = Convert.ToInt32(rd["id"].ToString());
                categoria.Nome = rd["nome"].ToString();
            }

            this.conn.Close();


            return categoria;
        }
        public List<Categoria> ListarTodos()
        {
            List<Categoria> categorias = new List<Categoria>();
            var command = new SqlCommand("select id, nome from categorias", this.conn);
                this.conn.Open();

            using (SqlDataReader rd = command.ExecuteReader())
            {
                while (rd.Read())
                {
                    Categoria categoria = new Categoria()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Nome = rd["nome"].ToString(),
                    };

                    categorias.Add(categoria);

                }
            }
            this.conn.Close();
            return categorias;
        }
    }
   
}
