using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class SMediaDbContext : DbContext
    {
        public DbSet<Community> Community { get; set; }
        public DbSet<FavoritePost> FavoritePost { get; set; }
        public DbSet<FollowedCommunity> FollowedCommunity { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostPicture> PostPicture { get; set; }
        public DbSet<UserPicture> UserPicture { get; set; }
        public DbSet<Reaction> Reaction { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<FollowedUser> FollowedUser { get; set; }
        public DbSet<Viewed> Viewed { get; set; }

        public SMediaDbContext() { }

        public SMediaDbContext(DbContextOptions<SMediaDbContext> options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Color)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Picture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.OwnerId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.Active)
                    .HasColumnType("bit")
                    .IsRequired();

                entity.HasOne(e => e.Owner)
                    .WithMany(y => y.MyCommunity)
                    .HasConstraintName("FK__Community__Owner__286302EC");
            });

            modelBuilder.Entity<FavoritePost>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.PostId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(y => y.FavoritePost)
                    .HasConstraintName("FK__FavoriteP__UserI__48CFD27E");

                entity.HasOne(e => e.Post)
                    .WithMany(y => y.FavoritePost)
                    .HasConstraintName("FK__FavoriteP__PostI__49C3F6B7");
            });

            modelBuilder.Entity<FollowedCommunity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FollowerId)
                    .IsRequired();

                entity.Property(e => e.CommunityId)
                    .IsRequired();

                entity.Property(e => e.DateOfFollow)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.HasOne(e => e.Follower)
                    .WithMany(y => y.FollowedCommunity)
                    .HasConstraintName("FK__FollowedC__Follo__3D5E1FD2");

                entity.HasOne(e => e.Community)
                    .WithMany(y => y.Follower)
                    .HasConstraintName("FK__FollowedC__Commu__3E52440B");


            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.Content)
                    .HasMaxLength(320)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.AuthorId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.CommunityId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.LastPostId)
                    .HasColumnType("bigint")
                    .IsRequired(false);

                entity.Property(e => e.Active)
                    .HasColumnType("bit")
                    .IsRequired();

                entity.HasOne(e => e.Author)
                    .WithMany(y => y.Post)
                    .HasConstraintName("FK__Post__AuthorId__2C3393D0");

                entity.HasOne(e => e.Community)
                    .WithMany(y => y.Post)
                    .HasConstraintName("FK__Post__CommunityI__2D27B809");

                entity.HasOne(e => e.LastPost)
                    .WithOne(y => y.BeforePost)
                    .HasConstraintName("FK__Post__LastPostId__2E1BDC42");
            });

            modelBuilder.Entity<PostPicture>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.ServerPath)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.PostId)
                    .IsRequired();

                entity.HasOne(e=>e.Post)
                    .WithMany(y => y.PostPicture)
                    .HasConstraintName("FK__PostPictu__PostI__31EC6D26");
            });

            modelBuilder.Entity<UserPicture>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.ServerPath)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.Active)
                    .HasColumnType("bit")
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(y => y.UserPicture)
                    .HasConstraintName("FK__UserPictu__UserI__5CD6CB2B");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint")
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.PostId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.HasOne(e => e.Post)
                    .WithMany(y => y.Reaction)
                    .HasConstraintName("FK__Reaction__PostId__36B12243");

                entity.HasOne(e => e.User)
                    .WithMany(y => y.Reaction)
                    .HasConstraintName("FK__Reaction__UserId__35BCFE0A");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.NickName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.Active)
                    .HasColumnType("bit")
                    .IsRequired();
            });

            modelBuilder.Entity<FollowedUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.FollowerId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.FollowedId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.DateOfFollow)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.HasOne(e => e.Follower)
                    .WithMany(y => y.Followed)
                    .HasConstraintName("FK__UsuarioSe__Follo__412EB0B6");

                entity.HasOne(e => e.Followed)
                    .WithMany(y => y.Follower)
                    .HasConstraintName("FK__UsuarioSe__Follo__4222D4EF");
            });

            modelBuilder.Entity<Viewed>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.Property(e => e.PostId)
                    .HasColumnType("bigint")
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(y => y.Viewed)
                    .HasConstraintName("FK__Viewed__UserId__44FF419A");

                entity.HasOne(e => e.Post)
                    .WithMany(y => y.Viewed)
                    .HasConstraintName("FK__Viewed__PostId__45F365D3");
            }); 
        }           
    }
}
