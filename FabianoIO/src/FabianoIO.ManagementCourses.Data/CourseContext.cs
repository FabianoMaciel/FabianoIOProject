using FabianoIO.ManagementCourses.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FabianoIO.Core.Data;
using MediatR;
using FabianoIO.Core.DomainObjects;
using FabianoIO.GestaoAlunos.Data;
using FabianoIO.Core.Messages;

namespace FabianoIO.ManagementCourses.Data
{
    public class CourseContext(DbContextOptions<CourseContext> options,
                                    IMediator mediator) : DbContext(options), IUnitOfWork
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
        }

        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;

            if (success)
                await mediator.PublishDomainEvents(this);

            return success;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries<Entity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("CreatedDate").IsModified = false;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Property("CreatedDate").IsModified = false;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("Courses");
        }
    }

    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("Lessons");
        }
    }
}
