﻿// <auto-generated />
using System;
using AgendaAdvogado.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgendaAdvogado.Repository.Migrations
{
    [DbContext(typeof(AgendaAdvogadoContext))]
    [Migration("20190528230922_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("AgendaAdvogado.Domain.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cnpj");

                    b.Property<string>("Estado")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AgendaAdvogado.Domain.Processo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Estado")
                        .IsRequired();

                    b.Property<string>("NumeroProcesso")
                        .IsRequired();

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Processos");
                });

            modelBuilder.Entity("AgendaAdvogado.Domain.Processo", b =>
                {
                    b.HasOne("AgendaAdvogado.Domain.Cliente")
                        .WithMany("Processos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
