namespace Snake.Models
{
    public class PontuacaoDoJogador
    {
        public PontuacaoDoJogador(string nomeJogador,int pontos)
        {
            NomeJogador = nomeJogador;
            Pontos = pontos;
        }
        public string NomeJogador { get; set; }
        public int Pontos { get; set; }
    }
}