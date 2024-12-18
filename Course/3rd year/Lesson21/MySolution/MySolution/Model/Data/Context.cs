using Microsoft.EntityFrameworkCore;

namespace MySolution.Model;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(b => b._Author)
                .WithMany(a => a.Books)
                .HasForeignKey("AuthorId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Authors");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
        });
        
        modelBuilder.Entity<Reader>(entity =>
        {
            entity.ToTable("Readers");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(r => r.ReadedBooks)
                .WithMany();
        });
    }
}

