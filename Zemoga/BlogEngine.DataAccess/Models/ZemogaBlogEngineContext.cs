using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BlogEngine.DataAccess.Models
{
    public partial class ZemogaBlogEngineContext : DbContext
    {
        public ZemogaBlogEngineContext(DbContextOptions<ZemogaBlogEngineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPost).HasColumnName("Id_Post");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_Comment_Post");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdStatus).HasColumnName("Id_Status");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Published).HasColumnType("datetime");

                entity.Property(e => e.StatusComment)
                    .HasColumnName("Status_Comment")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_Post_Status");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRol).HasColumnName("Id_Rol");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_User_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
