﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesteASPNet.Infra.Context;

#nullable disable

namespace TesteASPNet.Infra.Migrations
{
    [DbContext(typeof(MySqlContext))]
    partial class MySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TesteASPNet.Domain.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expiration_date");

                    b.Property<DateTime>("ManufacturingDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("manufacturing_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("situation");

                    b.Property<string>("VendorCNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("vendor_cnpj");

                    b.Property<string>("VendorCode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("vendor_code");

                    b.Property<string>("VendorName")
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("vendor_name");

                    b.HasKey("Id");

                    b.ToTable("product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Produto teste",
                            ExpirationDate = new DateTime(2022, 12, 22, 18, 35, 0, 198, DateTimeKind.Local).AddTicks(5638),
                            ManufacturingDate = new DateTime(2022, 12, 21, 18, 35, 0, 198, DateTimeKind.Local).AddTicks(5630),
                            Status = 1,
                            VendorCNPJ = "11111111111111",
                            VendorCode = "FORNECE52",
                            VendorName = "Fornecedor de Produtos"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
