using System;
using System.Data.SqlClient;

namespace Persistencia
{
    public class Categoria
    {
        private SqlConnection conn;
        public Categoria(SqlConnection conn)
        {
            this.conn = conn;
        }
    }
}
