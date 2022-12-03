using System;
using static System.Console;
namespace SistemaFinanceiroSoN
{
    class Program
    {
        static void Main(string[] args)
        {
            int opc;
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
                            Write("LISTAR");
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
            ReadLine();
        }
    }
}
