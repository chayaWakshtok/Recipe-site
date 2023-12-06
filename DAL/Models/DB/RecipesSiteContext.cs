using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models.DB;

public partial class RecipesSiteContext : DbContext
{
    public RecipesSiteContext()
    {
    }

    public RecipesSiteContext(DbContextOptions<RecipesSiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Difficulty> Difficulties { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Instruction> Instructions { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=RecipesSite;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Difficulty>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_feedbacks");

            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Mark).HasMaxLength(50);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Feedbacks_Recipes");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Feedbacks_Users");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasOne(d => d.FromUserNavigation).WithMany(p => p.FollowFromUserNavigations)
                .HasForeignKey(d => d.FromUser)
                .HasConstraintName("FK_Follows_Users");

            entity.HasOne(d => d.ToUserNavigation).WithMany(p => p.FollowToUserNavigations)
                .HasForeignKey(d => d.ToUser)
                .HasConstraintName("FK_Follows_Users1");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Image1).HasColumnName("Image");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Images)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Images_Recipes");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Ingredients_Recipes");
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Instructions_Recipes");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.Property(e => e.VideoUrl).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Recipes_Categories");

            entity.HasOne(d => d.Difficulty).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.DifficultyId)
                .HasConstraintName("FK_Recipes_Difficulties");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Recipes_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
