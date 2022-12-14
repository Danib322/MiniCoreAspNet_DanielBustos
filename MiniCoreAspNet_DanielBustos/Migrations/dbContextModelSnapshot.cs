// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniCoreAspNet_DanielBustos.Models;

namespace MiniCoreAspNet_DanielBustos.Migrations
{
    [DbContext(typeof(dbContext))]
    partial class dbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiniCoreAspNet_DanielBustos.Models.Pase", b =>
                {
                    b.Property<int>("PaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinTentativo")
                        .HasColumnType("datetime2");

                    b.Property<string>("TtipoPase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<int?>("pasesRestantes")
                        .HasColumnType("int");

                    b.Property<double?>("saldoRestante")
                        .HasColumnType("float");

                    b.HasKey("PaseID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Pases");
                });

            modelBuilder.Entity("MiniCoreAspNet_DanielBustos.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MiniCoreAspNet_DanielBustos.Models.Pase", b =>
                {
                    b.HasOne("MiniCoreAspNet_DanielBustos.Models.Usuario", "Usuario")
                        .WithMany("Pases")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MiniCoreAspNet_DanielBustos.Models.Usuario", b =>
                {
                    b.Navigation("Pases");
                });
#pragma warning restore 612, 618
        }
    }
}
