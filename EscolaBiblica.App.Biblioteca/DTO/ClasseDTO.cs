namespace EscolaBiblica.App.Biblioteca.DTO
{
    public class ClasseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public int Congregacao { get; set; }
        public int Setor { get; set; }

        public override string ToString() => Nome;
    }
}
