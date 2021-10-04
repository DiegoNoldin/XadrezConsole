using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {

        public Cavalo (Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString ()
        {
            return "C";
        }

        private bool podMov (Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p==null||p.cor!=cor;
        }

        public override bool[,] possivelMov ()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //acima.direita 
            pos.defVal(posicao.linha+2, posicao.coluna+1);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            //acima.esquerda
            pos.defVal(posicao.linha+2, posicao.coluna-1);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            //direita.cima 
            pos.defVal(posicao.linha+1, posicao.coluna+2);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }
            //direita.baixo
            pos.defVal(posicao.linha-1, posicao.coluna+2);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            //abaixo.direita 
            pos.defVal(posicao.linha-2, posicao.coluna+1);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            //abaixo.esquerda
            pos.defVal(posicao.linha-2, posicao.coluna-1);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            //esquerda.cima
            pos.defVal(posicao.linha+1, posicao.coluna-2);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            //esquerda.baixo
            pos.defVal(posicao.linha-1, posicao.coluna-2);
            if (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
            }

            return mat;
        }
    }
}