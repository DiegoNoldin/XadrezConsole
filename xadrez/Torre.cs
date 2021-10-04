using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {

        public Torre (Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString ()
        {
            return "T";
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

            //acima 
            pos.defVal(posicao.linha-1, posicao.coluna);
            while (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
                if (tab.peca(pos)!=null&&tab.peca(pos).cor!=cor)
                {
                    break;
                }
                pos.linha=pos.linha-1;
            }
            //abaixo 
            pos.defVal(posicao.linha+1, posicao.coluna);
            while (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
                if (tab.peca(pos)!=null&&tab.peca(pos).cor!=cor)
                {
                    break;
                }
                pos.linha=pos.linha+1;
            }
            //direita 
            pos.defVal(posicao.linha, posicao.coluna+1);
            while (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
                if (tab.peca(pos)!=null&&tab.peca(pos).cor!=cor)
                {
                    break;
                }
                pos.coluna=pos.coluna+1;
            }
            //esquerda 
            pos.defVal(posicao.linha, posicao.coluna-1);
            while (tab.validPos(pos)&&podMov(pos))
            {
                mat[pos.linha, pos.coluna]=true;
                if (tab.peca(pos)!=null&&tab.peca(pos).cor!=cor)
                {
                    break;
                }
                pos.coluna=pos.coluna-1;
            }


            return mat;
        }
    }
}
