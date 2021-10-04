using tabuleiro;
using xadrez;

namespace XadrezConsole.xadrez
{
    class Peao : Peca
    {

        private PartidaChess partida;

        public Peao (Tabuleiro tab, Cor cor, PartidaChess partida) : base(tab, cor)
        {
            this.partida=partida;
        }
        public override string ToString ()
        {
            return "p";
        }
        private bool enemyExists (Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p!=null&&p.cor!=cor;
        }

        private bool livre (Posicao pos)
        {
            return tab.peca(pos)==null;
        }

        public override bool[,] possivelMov ()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //passo

            if (cor==Cor.Branca)
            {

                pos.defVal(posicao.linha-1, posicao.coluna);
                if (tab.validPos(pos)&&livre(pos))
                {
                    mat[pos.linha, pos.coluna]=true;
                }
                pos.defVal(posicao.linha-2, posicao.coluna);
                if (tab.validPos(pos)&&livre(pos)&&qteMov==0)
                {
                    mat[pos.linha, pos.coluna]=true;
                }
                pos.defVal(posicao.linha-1, posicao.coluna-1);
                if (tab.validPos(pos)&&enemyExists(pos))
                {
                    mat[pos.linha, pos.coluna]=true;
                }
                pos.defVal(posicao.linha-1, posicao.coluna+1);
                if (tab.validPos(pos)&&enemyExists(pos))
                {
                    mat[pos.linha, pos.coluna]=true;
                }

                //SpecialMove En Passant: Brancas

                if (posicao.linha==3)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna-1);
                    if (tab.validPos(esquerda)&&enemyExists(esquerda)&&tab.peca(esquerda)==partida.vuneravelEnPassant)
                    {
                        mat[esquerda.linha-1, esquerda.coluna]=true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna+1);
                    if (tab.validPos(direita)&&enemyExists(direita)&&tab.peca(direita)==partida.vuneravelEnPassant)
                    {
                        mat[direita.linha-1, direita.coluna]=true;
                    }
                }

            } else
            {
                pos.defVal(posicao.linha+1, posicao.coluna);
                if (tab.validPos(pos)&&livre(pos))
                {
                    mat[pos.linha, pos.coluna]=true;
                }
                pos.defVal(posicao.linha+2, posicao.coluna);
                if (tab.validPos(pos)&&livre(pos)&&qteMov==0)
                {
                    mat[pos.linha, pos.coluna]=true;
                }
                pos.defVal(posicao.linha+1, posicao.coluna-1);
                if (tab.validPos(pos)&&enemyExists(pos))
                {
                    mat[pos.linha, pos.coluna]=true;
                }
                pos.defVal(posicao.linha+1, posicao.coluna+1);
                if (tab.validPos(pos)&&enemyExists(pos))
                {
                    mat[pos.linha, pos.coluna]=true;
                }

                //SpecialMove En Passant:Pretas
                if (posicao.linha==4)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna-1);
                    if (tab.validPos(esquerda)&&enemyExists(esquerda)&&tab.peca(esquerda)==partida.vuneravelEnPassant)
                    {
                        mat[esquerda.linha+1, esquerda.coluna]=true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna+1);
                    if (tab.validPos(direita)&&enemyExists(direita)&&tab.peca(direita)==partida.vuneravelEnPassant)
                    {
                        mat[direita.linha+1, direita.coluna]=true;
                    }
                }

            }
            return mat;

        }
    }
}
