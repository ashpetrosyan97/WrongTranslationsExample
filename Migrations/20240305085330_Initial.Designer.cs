﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WrongTranslationsExample;

#nullable disable

namespace WrongTranslationsExample.Migrations
{
    [DbContext(typeof(ExampleDbContext))]
    [Migration("20240305085330_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EFCore.Examples.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasPrecision(0)
                        .HasColumnType("timestamptz")
                        .HasColumnName("end_date");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDate")
                        .HasPrecision(0)
                        .HasColumnType("timestamptz")
                        .HasColumnName("start_date");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_orders_user_id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("EFCore.Examples.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("EFCore.Examples.Models.WorkingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("day");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("end_time");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("start_time");

                    b.Property<int>("WorkingSchemeId")
                        .HasColumnType("integer")
                        .HasColumnName("working_scheme_id");

                    b.HasKey("Id")
                        .HasName("pk_working_days");

                    b.HasIndex("WorkingSchemeId", "Day")
                        .IsUnique()
                        .HasDatabaseName("ix_working_days_working_scheme_id_day");

                    b.ToTable("working_days", (string)null);
                });

            modelBuilder.Entity("EFCore.Examples.Models.WorkingScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_working_scheme");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_working_scheme_user_id");

                    b.ToTable("working_scheme", (string)null);
                });

            modelBuilder.Entity("WrongTranslationsExample.Result", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.ToTable("result", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("EFCore.Examples.Models.Order", b =>
                {
                    b.HasOne("EFCore.Examples.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("fk_orders_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EFCore.Examples.Models.WorkingDay", b =>
                {
                    b.HasOne("EFCore.Examples.Models.WorkingScheme", "WorkingScheme")
                        .WithMany("WorkingDays")
                        .HasForeignKey("WorkingSchemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_working_days_working_scheme_working_scheme_id");

                    b.Navigation("WorkingScheme");
                });

            modelBuilder.Entity("EFCore.Examples.Models.WorkingScheme", b =>
                {
                    b.HasOne("EFCore.Examples.Models.User", "User")
                        .WithOne("WorkingScheme")
                        .HasForeignKey("EFCore.Examples.Models.WorkingScheme", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_working_scheme_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EFCore.Examples.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("WorkingScheme");
                });

            modelBuilder.Entity("EFCore.Examples.Models.WorkingScheme", b =>
                {
                    b.Navigation("WorkingDays");
                });
#pragma warning restore 612, 618
        }
    }
}
