using System;
using tabuleiro;
using xadrez;


namespace XadrezConsole
{
    class Program
    {
        static void Main (string[] args)
        {

            try
            {
                PartidaChess partida = new PartidaChess();
                while (!partida.terminada)
                {
                    try
                    {
                        Tela.printPartida(partida);
                        Console.WriteLine();
                        Console.Write("Origem:");
                        Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                        partida.validOrigem(origem);
                        Console.WriteLine();
                        bool[,] possivelPos = partida.tab.peca(origem).possivelMov();
                        Tela.printTabuleiro(partida.tab,possivelPos,partida);

                        string aux2 = Tela.OrgTabLine(origem);
                        string aux1 = Tela.OrgTabColum(origem);
                        Tela.printCapt(partida);
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
                        }

                        Console.WriteLine();
                        Console.WriteLine("Origem:"+aux2+aux1);
                        Console.Write("Destino:");

                        Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                        partida.validDestino(origem,destino);
                        partida.realizaJog(origem,destino);

                    } catch (tabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }
            } catch (tabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
