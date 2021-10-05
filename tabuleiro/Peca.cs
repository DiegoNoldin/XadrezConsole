
namespace tabuleiro
{
    abstract class Peca
    {

        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMov { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca (Tabuleiro tab,Cor cor)
        {
            this.posicao=null;
            this.tab=tab;
            this.cor=cor;
            this.qteMov=0;

        }
        public void incrementMove ()
        {
            qteMov++;
        }

        public void decrementMove ()
        {
            qteMov--;
        }



        public bool existMovPoss ()
        {
            bool[,] mat = possivelMov();
            for (int i = 0;i<tab.linhas;i++)
            {
                for (int j = 0;j<tab.colunas;j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool possivelMov (Posicao pos)
        {
            return possivelMov()[pos.linha,pos.coluna];
        }
        public abstract bool[,] possivelMov ();
    }
}
