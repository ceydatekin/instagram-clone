using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Instagram.InstagramContext
{
    public partial class instagramContext : DbContext
    {
        public instagramContext()
        {
        }

        public instagramContext(DbContextOptions<instagramContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Photograph> Photographs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userfollower> Userfollowers { get; set; }
        public virtual DbSet<Userphotograph> Userphotographs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Database=instagram;Port=5432;User Id=postgres;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_Turkey.1254");

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("like");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 999999999L, null, null);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Userphotographid).HasColumnName("userphotographid");
            });

            modelBuilder.Entity<Photograph>(entity =>
            {
                entity.ToTable("photograph");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 9999999L, null, null);

                entity.Property(e => e.Photograph1).HasColumnName("photograph");

                entity.Property(e => e.Photographtext).HasColumnName("photographtext");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999999L, null, null);

                entity.Property(e => e.Mail).HasColumnName("mail");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.Property(e => e.Telno).HasColumnName("telno");

                entity.Property(e => e.Username).HasColumnName("username");

                entity.Property(e => e.Userphotoid).HasColumnName("userphotoid");
            });

            modelBuilder.Entity<Userfollower>(entity =>
            {
                entity.ToTable("userfollowers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999999L, null, null);

                entity.Property(e => e.Followedid).HasColumnName("followedid");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Userphotograph>(entity =>
            {
                entity.ToTable("userphotograph");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 999999999L, null, null);

                entity.Property(e => e.Photographid).HasColumnName("photographid");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
