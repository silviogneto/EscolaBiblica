namespace EscolaBiblica.API.DAL.Modelos
{
    public class ProfessorClasse
    {
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public int ClasseId { get; set; }
        public Classe Classe { get; set; }
    }
}
