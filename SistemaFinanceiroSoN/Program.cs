using System;
using System.Collections.Generic;
using static System.Console;
using Modelo;
using Persistencia;
using System.Data.SqlClient;
using ConsoleTables;

namespace SistemaFinanceiroSoN
{
    class Program
    {
        private List<Conta> contas;
        private List<Categoria> categorias;
        private ContaDAL conta;
        private CategoriaDAL    categoria;
        public Program()
        {
            string strConn = Db.Conexao.GetStringConnection();
            this.conta = new ContaDAL(new SqlConnection(strConn));
            this.categoria = new CategoriaDAL(new SqlConnection(strConn));
        }

        static void Main(string[] args)
        {
            int opc;

            Program p = new Program();
            

            do
            {
                Title = "CONTROLE FINANCEIRO SON";
                Uteis.MontaMenu();
                opc = Convert.ToInt32(ReadLine());
                if (!(opc >= 1 && opc <= 6))
                {
                    Clear();
                    BackgroundColor = ConsoleColor.Red;
                    ForegroundColor = ConsoleColor.White;
                    Uteis.MontarHeader("INFORME UMA OPÇÃO VALIDA",'X',30);
                    ResetColor();
                    
                }
                else
                {
               
                    Clear();
                    switch (opc)
                    {
                        case 1:
                            Title = "LISTAGEM DE CONTAS - CONTROLE FINANCEIRO SON";
                            Uteis.MontarHeader("LISTAGEM DE CONTAS");
                            p.contas = p.conta.ListarTodos();

                            //Usando TableConsole Biblioteca do C#
                            ConsoleTable table = new ConsoleTable("ID", "DESCRIÇÃO", "TIPO", "VALOR");
                            foreach(var c in p.contas)
                            {
                                table.AddRow(c.Id, c.Descricao, c.Tipo.Equals('R') ? "Receber" : "Pagar", String.Format("{0:c}", c.Valor));

                            }
                            table.Write();
                            ReadLine();
                            Clear();

                            break;
                        case 2:
                            Write("CADASTRAR");
                            break;
                        case 3:
                            Write("EDITAR");
                            break;
                        case 4:
                            Write("EXCLUIR");
                            break;
                        case 5:
                            Write("RELATÓRIO");
                            break;
                        default:
                            break;
                    }
                }

            } while (opc != 6);
        }
    }
}
