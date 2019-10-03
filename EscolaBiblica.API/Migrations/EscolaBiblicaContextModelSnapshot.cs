﻿// <auto-generated />
using System;
using EscolaBiblica.API.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EscolaBiblica.API.Migrations
{
    [DbContext(typeof(EscolaBiblicaContext))]
    partial class EscolaBiblicaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CongregacaoId");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("CongregacaoId");

                    b.ToTable("Aluno");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Aluno");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Chamada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Biblias");

                    b.Property<int>("ClasseId");

                    b.Property<DateTime>("Data")
                        .HasColumnType("date");

                    b.Property<decimal>("Oferta");

                    b.Property<int>("Revistas");

                    b.Property<int>("Visitantes");

                    b.HasKey("Id");

                    b.HasIndex("ClasseId");

                    b.HasIndex("Data", "ClasseId")
                        .IsUnique();

                    b.ToTable("Chamada");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Classe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CongregacaoId");

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CongregacaoId");

                    b.ToTable("Classe");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Congregacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("SetorNumero");

                    b.HasKey("Id");

                    b.HasIndex("SetorNumero");

                    b.ToTable("Congregacao");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Garcia",
                            SetorNumero = 2
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Engenheiro Odebrecht",
                            SetorNumero = 2
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Glória",
                            SetorNumero = 2
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Hermann Huscher",
                            SetorNumero = 2
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Itapuí",
                            SetorNumero = 2
                        },
                        new
                        {
                            Id = 6,
                            Nome = "Zendron",
                            SetorNumero = 2
                        });
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<string>("Bairro")
                        .IsRequired();

                    b.Property<string>("CEP")
                        .IsRequired();

                    b.Property<string>("Cidade")
                        .IsRequired();

                    b.Property<string>("Complemento");

                    b.Property<string>("DescEndereco")
                        .IsRequired();

                    b.Property<string>("Estado")
                        .IsRequired();

                    b.Property<int>("Numero");

                    b.Property<int>("TipoEndereco");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Matricula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<int>("ClasseId");

                    b.Property<DateTime>("DataMatricula")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataTerminoMatricula")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("ClasseId");

                    b.ToTable("Matricula");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Presenca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChamadaId");

                    b.Property<int>("IdAluno");

                    b.Property<string>("NomeAluno")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ChamadaId");

                    b.ToTable("Presenca");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.ProfessorClasse", b =>
                {
                    b.Property<int>("ProfessorId");

                    b.Property<int>("ClasseId");

                    b.HasKey("ProfessorId", "ClasseId");

                    b.HasIndex("ClasseId");

                    b.ToTable("ProfessorClasse");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Setor", b =>
                {
                    b.Property<int>("Numero");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Numero");

                    b.ToTable("Setor");

                    b.HasData(
                        new
                        {
                            Numero = 1,
                            Nome = "Sede"
                        },
                        new
                        {
                            Numero = 2,
                            Nome = "Garcia"
                        },
                        new
                        {
                            Numero = 3,
                            Nome = "Badenfurt"
                        },
                        new
                        {
                            Numero = 4,
                            Nome = "Fortaleza"
                        },
                        new
                        {
                            Numero = 5,
                            Nome = "Escola Agrícola"
                        },
                        new
                        {
                            Numero = 6,
                            Nome = "Velha Central"
                        },
                        new
                        {
                            Numero = 7,
                            Nome = "Itoupava Central"
                        },
                        new
                        {
                            Numero = 8,
                            Nome = "Betânia"
                        },
                        new
                        {
                            Numero = 9,
                            Nome = "Nova Jerusalém"
                        },
                        new
                        {
                            Numero = 10,
                            Nome = "Betesda"
                        },
                        new
                        {
                            Numero = 11,
                            Nome = "Velha Grande"
                        },
                        new
                        {
                            Numero = 12,
                            Nome = "Progresso"
                        },
                        new
                        {
                            Numero = 13,
                            Nome = "Jerusalém"
                        },
                        new
                        {
                            Numero = 14,
                            Nome = "Araranguá"
                        },
                        new
                        {
                            Numero = 15,
                            Nome = "Itoupava Norte"
                        },
                        new
                        {
                            Numero = 16,
                            Nome = "Morell"
                        },
                        new
                        {
                            Numero = 17,
                            Nome = "Vila Itoupava"
                        },
                        new
                        {
                            Numero = 18,
                            Nome = "Moriá"
                        },
                        new
                        {
                            Numero = 19,
                            Nome = "Água Verde"
                        },
                        new
                        {
                            Numero = 20,
                            Nome = "América do Sol"
                        },
                        new
                        {
                            Numero = 21,
                            Nome = "Rua das Missões"
                        },
                        new
                        {
                            Numero = 22,
                            Nome = "Cidade Jardim"
                        },
                        new
                        {
                            Numero = 23,
                            Nome = "Pérola do Vale"
                        },
                        new
                        {
                            Numero = 24,
                            Nome = "Ristow"
                        },
                        new
                        {
                            Numero = 25,
                            Nome = "Jordão"
                        },
                        new
                        {
                            Numero = 26,
                            Nome = "Pedro Krauss"
                        });
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Nome");

                    b.Property<string>("Perfil")
                        .IsRequired();

                    b.Property<string>("Senha")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            Login = "silviogneto",
                            Nome = "Silvio Neto",
                            Perfil = "ADMIN",
                            Senha = "gQKeZEq2lKP+47IczeNqpjD3LJBCrA=="
                        });
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Professor", b =>
                {
                    b.HasBaseType("EscolaBiblica.API.DAL.Modelos.Aluno");

                    b.Property<int>("UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Professor");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Coordenador", b =>
                {
                    b.HasBaseType("EscolaBiblica.API.DAL.Modelos.Professor");

                    b.ToTable("Coordenador");

                    b.HasDiscriminator().HasValue("Coordenador");
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Aluno", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Congregacao", "Congregacao")
                        .WithMany()
                        .HasForeignKey("CongregacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Chamada", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Classe", "Classe")
                        .WithMany()
                        .HasForeignKey("ClasseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Classe", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Congregacao", "Congregacao")
                        .WithMany()
                        .HasForeignKey("CongregacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Congregacao", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorNumero")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Endereco", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Aluno", "Aluno")
                        .WithMany("Enderecos")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Matricula", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Classe", "Classe")
                        .WithMany()
                        .HasForeignKey("ClasseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Presenca", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Chamada", "Chamada")
                        .WithMany("Presencas")
                        .HasForeignKey("ChamadaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.ProfessorClasse", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Classe", "Classe")
                        .WithMany("Professores")
                        .HasForeignKey("ClasseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Professor", "Professor")
                        .WithMany("Classes")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EscolaBiblica.API.DAL.Modelos.Professor", b =>
                {
                    b.HasOne("EscolaBiblica.API.DAL.Modelos.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
