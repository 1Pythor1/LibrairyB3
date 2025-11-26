

using Microsoft.EntityFrameworkCore;
using Book.Models;

namespace DAL
{
    public class LibrairyContext : DbContext
    {
        private const int _MAX_LENGTH = 100;
        public DbSet<Storie> Storie { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Choice> Choice { get; set; }

        public LibrairyContext(DbContextOptions<LibrairyContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

                            
            modelBuilder.Entity<Storie>(entity =>
            {
                //Id
                entity.HasKey(u => u.Id);                
                entity.Property(u => u.Id)
                    .ValueGeneratedOnAdd();

                //Title
                entity.Property(u => u.Title)
                    .HasMaxLength(_MAX_LENGTH)
                    .IsRequired();
                
                //Description
                entity.Property(u => u.Description)
                    .HasMaxLength(_MAX_LENGTH * 3)
                    .IsRequired();

                //Tags
                entity.Property(u => u.Tags)
                    .IsRequired(false);

                //Statut
                entity.Property(u => u.Statut)
                    .HasDefaultValue(false)
                    .IsRequired();

                //First Page
                entity.Property(u => u.FirstPage)
                    .IsRequired(false);

                entity.HasOne<Page>()
                  .WithMany()
                  .HasForeignKey(s => s.FirstPage)
                  .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Page>(entity =>
            {
                //Id
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id) 
                    .ValueGeneratedOnAdd();

                //Texte
                entity.Property(u => u.Text)
                    .HasMaxLength(_MAX_LENGTH)
                    .IsRequired();

                //Is End
                entity.Property(u => u.IsEnd)
                    .HasDefaultValue(false)
                    .IsRequired();

                //Storie Id
                entity.Property(u => u.StorieId)
                    .IsRequired();

                entity.HasOne<Storie>()
                    .WithMany()
                    .HasForeignKey(p => p.StorieId)
                    .OnDelete(DeleteBehavior.Cascade);

                //Choices
                entity.HasMany(p => p.Choices)
                  .WithOne()
                  .HasForeignKey(c => c.PageId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Choice>(entity =>
            {
                //Id
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                    .ValueGeneratedOnAdd();

                //Description
                entity.Property(u => u.Id)
                    .HasMaxLength(_MAX_LENGTH)
                    .IsRequired();

                //Next Page
                entity.HasOne<Page>()
                  .WithMany()
                  .HasForeignKey(s => s.NextPage)
                  .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}
/*
dotnet ef migrations add InitialCreate --project DAL --startup-project Book
dotnet ef database update --project DAL --startup-project Book
 */