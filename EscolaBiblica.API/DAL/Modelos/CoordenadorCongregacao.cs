namespace EscolaBiblica.API.DAL.Modelos
{
    public class CoordenadorCongregacao
    {
        public int CoordenadorId { get; set; }
        public Coordenador Coordenador { get; set; }

        public int CongregacaoId { get; set; }
        public Congregacao Congregacao { get; set; }
    }
}
