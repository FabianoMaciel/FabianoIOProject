﻿// <auto-generated />
using System;
using FabianoIO.ManagementCourses.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FabianoIO.ManagementCourses.Data.Migrations
{
    [DbContext(typeof(CoursesContext))]
    [Migration("20250701133128_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.15");

            modelBuilder.Entity("FabianoIO.ManagementCourses.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("FabianoIO.ManagementCourses.Domain.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalHours")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lessons", (string)null);
                });

            modelBuilder.Entity("FabianoIO.ManagementCourses.Domain.ProgressLesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProgressionStatus")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("ProgressLessons", (string)null);
                });

            modelBuilder.Entity("FabianoIO.ManagementCourses.Domain.Lesson", b =>
                {
                    b.HasOne("FabianoIO.ManagementCourses.Domain.Course", null)
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FabianoIO.ManagementCourses.Domain.ProgressLesson", b =>
                {
                    b.HasOne("FabianoIO.ManagementCourses.Domain.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("FabianoIO.ManagementCourses.Domain.Course", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
