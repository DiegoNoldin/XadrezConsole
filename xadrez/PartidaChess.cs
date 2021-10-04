using System;
using System.Collections.Generic;
using tabuleiro;
using XadrezConsole.xadrez;


namespace xadrez
{
    class PartidaChess
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor playerActual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vuneravelEnPassant { get; private set; }

        public PartidaChess ()
        {
            tab=new Tabuleiro(8, 8);
            turno=1;
            playerActual=Cor.Branca;
            terminada=false;
            vuneravelEnPassant=null;
            pecas=new HashSet<Peca>();
            capturadas=new HashSet<Peca>();
            colocarPecas();
        }

        #region Executa Movimento
        public Peca ExecMove (Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementMove();
            Peca pecaCapt = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapt!=null)
            {
                capturadas.Add(pecaCapt);
            }
            #endregion

            #region special move en passant e roque
            //special move

            //MiniRoque
            if (p is Rei&&destino.coluna==origem.coluna+2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna+3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna+1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementMove();
                tab.colocarPeca(T, destinoT);
            }

            //BigRoque
            if (p is Rei&&destino.coluna==origem.coluna-2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna-4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna-1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementMove();
                tab.colocarPeca(T, destinoT);
            }

            //En Passant
            if (p is Peao)
            {
                if (origem.coluna!=destino.coluna&&pecaCapt==null)
                {
                    Posicao posP;
                    if (p.cor==Cor.Branca)
                    {
                        posP=new Posicao(destino.linha+1, destino.coluna);
                    } else
                    {
                        posP=new Posicao(destino.linha-1, destino.coluna);
                    }
                    pecaCapt=tab.retirarPeca(posP);
                    capturadas.Add(pecaCapt);
                }
            }
            return pecaCapt;
        }
        #endregion

        #region Faz Jogada
        public void realizaJog (Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecMove(origem, destino);

            if (Xeque(playerActual))
            {
                desfazMov(origem, destino, pecaCapturada);
                throw new tabuleiroException("Você não pode se colocar em Xeque");
            }

            Peca p = tab.peca(destino);

            //SpecialMove Promotion

            if (p is Peao)
            {
                if ((p.cor==Cor.Branca&&destino.linha==0)||(p.cor==Cor.Preta&&destino.linha==7))
                {
                    p=tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Console.WriteLine("Promoção de peão: escolha qual peça deseja:");
                    Console.WriteLine("Cavalo(C),Bispo(B),Torre(T) ou Rainha(Q):");
                    string promo = Console.ReadLine();

                    if (promo=="c"||promo=="C")
                    {
                        Peca cavalo = new Cavalo(tab, p.cor);
                        tab.colocarPeca(cavalo, destino);
                        pecas.Add(cavalo);
                    }
                    if (promo=="b"||promo=="B")
                    {
                        Peca bispo = new Bispo(tab, p.cor);
                        tab.colocarPeca(bispo, destino);
                        pecas.Add(bispo);
                    }
                    if (promo=="t"||promo=="T")
                    {
                        Peca torre = new Torre(tab, p.cor);
                        tab.colocarPeca(torre, destino);
                        pecas.Add(torre);
                    }
                    if (promo=="q"||promo=="Q")
                    {
                        Peca rainha = new Rainha(tab, p.cor);
                        tab.colocarPeca(rainha, destino);
                        pecas.Add(rainha);
                    }

                }
            }

            if (Xeque(adversaria(playerActual)))
            {
                xeque=true;
            } else
            {
                xeque=false;
            }

            if (testXequeMate(adversaria(playerActual)))
            {
                terminada=true;
            } else
            {
                turno++;
                mudaPlayer();
            }

            //Special move En Passant

            if (p is Peao&&(destino.linha==origem.linha-2||destino.linha==origem.linha+2))
            {
                vuneravelEnPassant=p;
            } else
            {
                vuneravelEnPassant=null;
            }
        }
        #endregion


        #region Desfaz Jogada
        private void desfazMov (Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementMove();
            if (pecaCapturada!=null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            //miniroque
            if (p is Rei&&destino.coluna==origem.coluna+2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna+3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna+1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementMove();
                tab.colocarPeca(T, origemT);
            }

            //BigRoque
            if (p is Rei&&destino.coluna==origem.coluna-2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna-4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna-1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementMove();
                tab.colocarPeca(T, origemT);
            }

            //En Passant
            if (p is Peao)
            {
                if (origem.coluna!=destino.coluna&&pecaCapturada==vuneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor==Cor.Branca)
                    {
                        posP=new Posicao(3, destino.coluna);
                    } else
                    {
                        posP=new Posicao(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }
        #endregion

        #region Validação de Origem
        public void validOrigem (Posicao pos)
        {
            if (tab.peca(pos)==null)
            {
                throw new tabuleiroException("Não há peças nessa posição");
            }
            if (playerActual!=tab.peca(pos).cor)
            {
                throw new tabuleiroException("A peça escolhida não é sua");
            }
            if (!tab.peca(pos).existMovPoss())
            {
                throw new tabuleiroException("Não há movimentos possiveis");
            }
        }
        #endregion

        #region Validação de Destino
        public void validDestino (Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).possivelMov(destino))
            {
                throw new tabuleiroException("Posição de destino invalida");
            }

        }
        #endregion

        #region Implementação da troca de jogador
        private void mudaPlayer ()
        {
            if (playerActual==Cor.Branca)
            {
                playerActual=Cor.Preta;
            } else
            {
                playerActual=Cor.Branca;
            }
        }
        #endregion

        #region Identificação das peças capturadas
        public HashSet<Peca> pecasCapturadas (Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor==cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        #endregion

        #region Identificação das peças em jogo
        public HashSet<Peca> pecasEmJogo (Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor==cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        #endregion

        #region Identificação das peças adversarias
        private Cor adversaria (Cor cor)
        {
            if (cor==Cor.Branca)
            {
                return Cor.Preta;
            } else
            {
                return Cor.Branca;
            }

        }
        #endregion

        #region Identificação de peça ("Rei")
        private Peca rei (Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        #endregion

        #region Regra "Xeque"
        public bool Xeque (Cor cor)
        {
            Peca R = rei(cor);
            if (R==null)
            {
                throw new tabuleiroException("Não há rei da cor "+cor+" no tabuleiro");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.possivelMov();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Teste da condição XequeMate
        public bool testXequeMate (Cor cor)
        {
            if (!Xeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.possivelMov();
                for (int i = 0; i<tab.linhas; i++)
                {
                    for (int j = 0; j<tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapt = ExecMove(origem, destino);
                            bool testXeque = Xeque(cor);
                            desfazMov(origem, destino, pecaCapt);
                            if (!testXeque)
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            return true;
        }
        #endregion

        #region Implementação de uma nova peça(N)
        public void placeNewPeca (char coluna, int linha, Peca peca)
        {
            //pecas=new HashSet<Peca>();
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        #endregion

        #region Implementação das peças do xadrez
        private void colocarPecas ()
        {
            placeNewPeca('a', 8, new Torre(tab, Cor.Preta));
            placeNewPeca('b', 8, new Cavalo(tab, Cor.Preta));
            placeNewPeca('c', 8, new Bispo(tab, Cor.Preta));
            placeNewPeca('d', 8, new Rainha(tab, Cor.Preta));
            placeNewPeca('e', 8, new Rei(tab, Cor.Preta, this));
            placeNewPeca('f', 8, new Bispo(tab, Cor.Preta));
            placeNewPeca('g', 8, new Cavalo(tab, Cor.Preta));
            placeNewPeca('h', 8, new Torre(tab, Cor.Preta));

            placeNewPeca('a', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('b', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('c', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('d', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('e', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('f', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('g', 7, new Peao(tab, Cor.Preta, this));
            placeNewPeca('h', 7, new Peao(tab, Cor.Preta, this));

            placeNewPeca('a', 1, new Torre(tab, Cor.Branca));
            placeNewPeca('b', 1, new Cavalo(tab, Cor.Branca));
            placeNewPeca('c', 1, new Bispo(tab, Cor.Branca));
            placeNewPeca('d', 1, new Rainha(tab, Cor.Branca));
            placeNewPeca('e', 1, new Rei(tab, Cor.Branca, this));
            placeNewPeca('f', 1, new Bispo(tab, Cor.Branca));
            placeNewPeca('g', 1, new Cavalo(tab, Cor.Branca));
            placeNewPeca('h', 1, new Torre(tab, Cor.Branca));

            placeNewPeca('a', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('b', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('c', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('d', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('e', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('f', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('g', 2, new Peao(tab, Cor.Branca, this));
            placeNewPeca('h', 2, new Peao(tab, Cor.Branca, this));
        }
        #endregion
    }
}
