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

                            Title = "NOVA CONTA - CONTROLE FINANCEIRO SON";
                            Uteis.MontarHeader("CADASTRANDO UMA NOVA CONTA");

                            string descricao = "";
                            do
                            {
                                Write("Informe a descrição da conta: ");
                                descricao = ReadLine();

                                if (descricao.Equals(""))
                                {
                                    BackgroundColor = ConsoleColor.Red;
                                    ForegroundColor = ConsoleColor.White;
                                    Uteis.MontarHeader("INFORME UMA DESCRICAO PARA A CONTA", '*', 30);
                                    ResetColor();
                                }
                            } while (descricao.Equals(""));
                            


                            Write("Informe o valor: ");
                            double valor = Convert.ToDouble(ReadLine());

                            Write("Informe o tipo: (R para Receber ou P para Pagar): ");
                            char tipo = Convert.ToChar(ReadLine());

                            Write("Informe a Data de vencimento (dd/mm/aaaa): ");
                            DateTime dataVencimento = DateTime.Parse(ReadLine());

                            WriteLine("Selecione uma Categoria pelo ID: \n");
                            p.categorias = p.categoria.ListarTodos();
                             table = new ConsoleTable("ID", "Nome");
                            foreach (var c in p.categorias)
                            {
                                table.AddRow(c.Id, c.Nome);

                            }
                            table.Write();
                            Write("Categoria: ");
                            int cat_id = Convert.ToInt32(ReadLine());
                            Categoria categoria_cadastro = p.categoria.GetCategoria(cat_id);

                            Conta conta = new Conta()
                            {
                                Descricao = descricao,
                                Valor = valor,
                                Tipo = tipo,
                                DataVencimento = dataVencimento,
                                Categoria = categoria_cadastro

                            };
                            p.conta.Salvar(conta);

                            BackgroundColor = ConsoleColor.DarkGreen;
                            ForegroundColor = ConsoleColor.White;
                            Uteis.MontarHeader("CONTA SALVA COM SUCESSO", '+', 30);
                            ResetColor();

                            ReadLine();
                            Clear();

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
