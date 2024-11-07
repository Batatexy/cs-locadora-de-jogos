﻿// <auto-generated />
using System;
using LocadoraDejogos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LocadoraDejogos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241103024233_AdicionarImagemUrl")]
    partial class AdicionarImagemUrl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("LocadoraDejogos.Models.Alugueis", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClienteID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FuncionarioID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("JogoID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("FuncionarioID");

                    b.HasIndex("JogoID");

                    b.ToTable("Alugueis");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Clientes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Consoles", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Ano")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fabricante")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Geracao")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Consoles");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Funcionarios", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Jogos", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Ano")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ConsoleID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Desenvolvedor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Distribuidora")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genero")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagemURL")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Unidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ConsoleID");

                    b.ToTable("Jogos");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.JogosConsoles", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ConsoleID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("JogoID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ConsoleID");

                    b.HasIndex("JogoID");

                    b.ToTable("JogosConsoles");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Alugueis", b =>
                {
                    b.HasOne("LocadoraDejogos.Models.Clientes", "Clientes")
                        .WithMany("Alugueis")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LocadoraDejogos.Models.Funcionarios", "Funcionarios")
                        .WithMany("Alugueis")
                        .HasForeignKey("FuncionarioID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LocadoraDejogos.Models.Jogos", "Jogos")
                        .WithMany("Alugueis")
                        .HasForeignKey("JogoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Clientes");

                    b.Navigation("Funcionarios");

                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Jogos", b =>
                {
                    b.HasOne("LocadoraDejogos.Models.Consoles", "Consoles")
                        .WithMany("Jogos")
                        .HasForeignKey("ConsoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Consoles");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.JogosConsoles", b =>
                {
                    b.HasOne("LocadoraDejogos.Models.Consoles", "Consoles")
                        .WithMany("JogosConsoles")
                        .HasForeignKey("ConsoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LocadoraDejogos.Models.Jogos", "Jogos")
                        .WithMany("JogosConsoles")
                        .HasForeignKey("JogoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Consoles");

                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Clientes", b =>
                {
                    b.Navigation("Alugueis");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Consoles", b =>
                {
                    b.Navigation("Jogos");

                    b.Navigation("JogosConsoles");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Funcionarios", b =>
                {
                    b.Navigation("Alugueis");
                });

            modelBuilder.Entity("LocadoraDejogos.Models.Jogos", b =>
                {
                    b.Navigation("Alugueis");

                    b.Navigation("JogosConsoles");
                });
#pragma warning restore 612, 618
        }
    }
}