namespace EscolaBiblica.API.DAL.Modelos
{
    public class Professor : Aluno
    {
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
