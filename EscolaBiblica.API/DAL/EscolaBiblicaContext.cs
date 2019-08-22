using System.Linq;
using EscolaBiblica.API.DAL.Modelos;
using EscolaBiblica.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EscolaBiblica.API.DAL
{
    public class EscolaBiblicaContext : DbContext
    {
        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<Classe> Classes { get; set; }
        public virtual DbSet<Congregacao> Congregacoes { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Matricula> Matriculas { get; set; }
        public virtual DbSet<Professor> Professores { get; set; }
        public virtual DbSet<Setor> Setores { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        public EscolaBiblicaContext(DbContextOptions<EscolaBiblicaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nome = "Silvio Neto", Login = "silviogneto", Senha = Hash.GerarHash("senha"), Perfil = Perfil.Admin, Ativo = true }
            );

            #region Setores && Congregacoes

            modelBuilder.Entity<Setor>().HasData(
                new Setor { Numero = 1, Nome = "Sede" },
                new Setor { Numero = 2, Nome = "Garcia" },
                new Setor { Numero = 3, Nome = "Badenfurt" },
                new Setor { Numero = 4, Nome = "Fortaleza" },
                new Setor { Numero = 5, Nome = "Escola Agrícola" },
                new Setor { Numero = 6, Nome = "Velha Central" },
                new Setor { Numero = 7, Nome = "Itoupava Central" },
                new Setor { Numero = 8, Nome = "Betânia" },
                new Setor { Numero = 9, Nome = "Nova Jerusalém" },
                new Setor { Numero = 10, Nome = "Betesda" },
                new Setor { Numero = 11, Nome = "Velha Grande" },
                new Setor { Numero = 12, Nome = "Progresso" },
                new Setor { Numero = 13, Nome = "Jerusalém" },
                new Setor { Numero = 14, Nome = "Araranguá" },
                new Setor { Numero = 15, Nome = "Itoupava Norte" },
                new Setor { Numero = 16, Nome = "Morell" },
                new Setor { Numero = 17, Nome = "Vila Itoupava" },
                new Setor { Numero = 18, Nome = "Moriá" },
                new Setor { Numero = 19, Nome = "Água Verde" },
                new Setor { Numero = 20, Nome = "América do Sol" },
                new Setor { Numero = 21, Nome = "Rua das Missões" },
                new Setor { Numero = 22, Nome = "Cidade Jardim" },
                new Setor { Numero = 23, Nome = "Pérola do Vale" },
                new Setor { Numero = 24, Nome = "Ristow" },
                new Setor { Numero = 25, Nome = "Jordão" },
                new Setor { Numero = 26, Nome = "Pedro Krauss" }
            );

            modelBuilder.Entity<Congregacao>().HasData(
                new Congregacao { Id = 1, Nome = "Garcia", SetorNumero = 2 },
                new Congregacao { Id = 2, Nome = "Engenheiro Odebrecht", SetorNumero = 2 },
                new Congregacao { Id = 3, Nome = "Glória", SetorNumero = 2 },
                new Congregacao { Id = 4, Nome = "Hermann Huscher", SetorNumero = 2 },
                new Congregacao { Id = 5, Nome = "Itapuí", SetorNumero = 2 },
                new Congregacao { Id = 6, Nome = "Zendron", SetorNumero = 2 }
            );

            #endregion

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }

            var foreignKeysMatricula = modelBuilder.Model.GetEntityTypes(typeof(Matricula))?.SelectMany(x => x.GetForeignKeys());
            if (foreignKeysMatricula != null)
            {
                foreach (var foreignKey in foreignKeysMatricula)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
