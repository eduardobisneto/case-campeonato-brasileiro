namespace CampeonatoBrasileiroAPI.Dominio.Entity
{
    public class Campeonato
    {
        public short Ano { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; }
        public short Posicao { get; set; }
        public short Pontos { get; set; }
        public short Jogos { get; set; }
        public short Vitorias { get; set; }
        public short Empates { get; set; }
        public short Derrotas { get; set; }
        public short GolsAFavor { get; set; }
        public short GolsContra { get; set; }
    }
}
