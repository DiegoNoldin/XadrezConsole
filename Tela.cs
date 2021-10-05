using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;
using XadrezConsole.xadrez;

namespace XadrezConsole
{
    class Tela
    {
        public static string OrgTabColum (Posicao pos)
        {
            string[] aux = pos.ToString().Split(',');
            bool i = false;
            while (i==false)
            {
                if (aux[0]=="0")
                {
                    i=true;
                    return "8";

                } else if (aux[0]=="1")
                {
                    i=true;
                    return "7";

                } else if (aux[0]=="2")
                {
                    i=true;
                    return "6";

                } else if (aux[0]=="3")
                {
                    i=true;
                    return "5";

                } else if (aux[0]=="4")
                {
                    i=true;
                    return "4";

                } else if (aux[0]=="5")
                {
                    i=true;
                    return "3";

                } else if (aux[0]=="6")
                {
                    i=true;
                    return "2";

                } else
                {
                    i=true;
                    return "1";
                }
            }
            return aux[0];
        }
        public static string OrgTabLine (Posicao pos)
        {
            string[] aux = pos.ToString().Split(',');
            bool j = false;
            while (j==false)
            {
                if (aux[1]=="0")
                {
                    j=true;
                    return "h";

                } else if (aux[1]=="1")
                {
                    j=true;
                    return "g";

                } else if (aux[1]=="2")
                {
                    j=true;
                    return "f";

                } else if (aux[1]=="3")
                {
                    j=true;
                    return "e";

                } else if (aux[1]=="4")
                {
                    j=true;
                    return "d";

                } else if (aux[1]=="5")
                {
                    j=true;
                    return "c";

                } else if (aux[1]=="6")
                {
                    j=true;
                    return "b";

                } else
                {
                    j=true;
                    return "a";
                }
            }
            return aux[1];
        }

        public static void printPartida (PartidaChess partida)
        {

            printTabuleiro(partida.tab);

            Console.WriteLine();
            printCapt(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: "+partida.turno);
            if (!partida.terminada)
            {
                Console.Write("Aguardando jogada da peça: ");
                if (partida.playerActual==Cor.Branca)
                {

                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor=ConsoleColor.White;
                    Console.Write(partida.playerActual);
                    Console.ForegroundColor=aux;

                } else if (partida.playerActual==Cor.Preta)
                {

                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor=ConsoleColor.DarkYellow;
                    Console.Write(partida.playerActual);
                    Console.ForegroundColor=aux;
                }


                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!!");
                }
            } else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: Peça "+partida.playerActual);
            }
        }

        public static void printCapt (PartidaChess partida)
        {
            Console.WriteLine("Peças capturadas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor=ConsoleColor.White;
            Console.Write("Brancas: ");
            printConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.ForegroundColor=aux;
            Console.WriteLine();
            Console.ForegroundColor=ConsoleColor.DarkYellow;
            Console.Write("Pretas: ");
            printConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor=aux;

            Console.WriteLine();
        }

        public static void printConjunto (HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x+" ");
            }
            Console.Write("]");
        }

        public static void printTabuleiro (Tabuleiro tab)
        {

            Console.Clear();
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor=ConsoleColor.DarkGreen;
            Console.WriteLine(" ¢ A B C D E F G H ¢");
            Console.ForegroundColor=aux;
            for (int i = 0;i<tab.linhas;i++)
            {
                Console.ForegroundColor=ConsoleColor.DarkGreen;
                Console.Write(" ");
                Console.Write(8-i+" ");
                Console.ForegroundColor=aux;
                for (int j = 0;j<tab.colunas;j++)
                {
                    Tela.printPeca(tab.peca(i,j));
                }
                Console.ForegroundColor=ConsoleColor.DarkGreen;
                Console.Write(8-i+" ");
                Console.ForegroundColor=aux;
                Console.WriteLine();
            }
            Console.ForegroundColor=ConsoleColor.DarkGreen;
            Console.WriteLine(" ¢ A B C D E F G H ¢");
            Console.ForegroundColor=aux;
            Console.WriteLine();

        }

        public static void printTabuleiro (Tabuleiro tab,bool[,] possivelPos,PartidaChess partida)
        {

            Console.Clear();
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor=ConsoleColor.DarkGreen;
            Console.WriteLine(" ¢ A B C D E F G H ¢");
            Console.ForegroundColor=aux;
            ConsoleColor fundoOrig = Console.BackgroundColor;
            ConsoleColor fundoMark = ConsoleColor.DarkGray;

            for (int i = 0;i<tab.linhas;i++)
            {
                Console.ForegroundColor=ConsoleColor.DarkGreen;
                Console.Write(" ");
                Console.Write(8-i+" ");
                Console.ForegroundColor=aux;
                Console.BackgroundColor=fundoOrig;
                for (int j = 0;j<tab.colunas;j++)
                {

                    if (possivelPos[i,j])
                    {
                        Console.BackgroundColor=fundoMark;
                    } else
                    {
                        Console.BackgroundColor=fundoOrig;
                    }
                    Tela.printPeca(tab.peca(i,j));
                    Console.ForegroundColor=aux;
                    Console.BackgroundColor=fundoOrig;
                }
                Console.ForegroundColor=ConsoleColor.DarkGreen;
                Console.Write(8-i+" ");
                Console.ForegroundColor=aux;
                Console.WriteLine();


            }
            Console.ForegroundColor=ConsoleColor.DarkGreen;
            Console.WriteLine(" ¢ A B C D E F G H ¢");
            Console.ForegroundColor=aux;
            Console.WriteLine();
            Console.BackgroundColor=fundoOrig;

        }
        public static PosicaoXadrez LerPosicaoXadrez ()
        {
            try
            {
                string s = Console.ReadLine();
                char coluna = s[0];
                int linha = int.Parse(s[1]+" ");
                return new PosicaoXadrez(coluna,linha);
            } catch (Exception)
            {
                throw new tabuleiroException("Posição Invalida");
            }
        }

        public static void printPeca (Peca peca)
        {

            if (peca==null)
            {
                Console.Write("_ ");
            } else
            {
                if (peca.cor==Cor.Branca)
                {

                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor=ConsoleColor.White;
                    Console.Write(peca+" ");
                    Console.ForegroundColor=aux;

                } else if (peca.cor==Cor.Preta)
                {

                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor=ConsoleColor.DarkYellow;
                    Console.Write(peca+" ");
                    Console.ForegroundColor=aux;
                }
            }
        }
    }
}
