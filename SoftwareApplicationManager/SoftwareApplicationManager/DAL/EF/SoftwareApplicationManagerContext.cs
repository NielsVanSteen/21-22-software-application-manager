using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.DAL.EF
{
    public class SoftwareApplicationManagerContext : DbContext
    {

        // Properties.
        public DbSet<Developer> Developers { get; set; }
        public DbSet<SoftwareApplication> SoftwareApplications { get; set; }
        public DbSet<OperatingSystem> OperatingSystems { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        //public Address Address { get; set; } // no DbSet for 'Address' -> 'Address' is owned -> declaring a DbSet will create a separate table for 'Address'.

        // Constructor.
        public SoftwareApplicationManagerContext()
        {
            SoftwareApplicationInitializer.Initialize(this, false);
        } // SoftwareApplicationManagerContext.

        // Methods.
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlite(@"Data Source=../mydb.db").LogTo(message => Debug.WriteLine(message), LogLevel.Information);
            }

        } // OnConfiguring.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            DeclareRelationsForDeveloper(modelBuilder);
            DeclareRelationsForRating(modelBuilder);
            DeclareRelationsForSoftwareApplication(modelBuilder);
            DeclareRelationsForOperatingSystem(modelBuilder);

        } // OnModelCreating.
        
        
        #region MethodsForExplicitRelationDeclaration
        
        private void DeclareRelationsForDeveloper(ModelBuilder modelBuilder)
        {
            // The 'Address' class is owned by 'Developer', and 'Developer' has exactly 1 'Address'.
            modelBuilder.Owned<Address>();
            modelBuilder.Entity<Developer>().OwnsOne(d => d.Address);
        } // RelationsDeveloper.
        
        private void DeclareRelationsForRating(ModelBuilder modelBuilder)
        {
            // The one-to-many relation between 'Rating' and 'Developer'.
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Developer)
                .WithMany(d => d.RatedApplications)
                .HasForeignKey("FK_developerId")
                .IsRequired(true);
            
            // The one-to-many relation between 'Rating' and 'SoftwareApplication'.
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.SoftwareApplication)
                .WithMany(s => s.Ratings)
                .HasForeignKey("FK_softwareApplicationId")
                .IsRequired(true);

            // Class 'Rating' has a composite primary key made of: 'Developer' and 'SoftwareApplication'.

            modelBuilder.Entity<Rating>().HasKey("FK_softwareApplicationId" , "FK_developerId");
            //modelBuilder.Entity<Rating>() .HasKey(r => r.RatingId);

        } // DeclareRelationsForRating.
        
        private void DeclareRelationsForSoftwareApplication(ModelBuilder modelBuilder)
        {
            // Many-to-many relation between 'SoftwareApplication' and 'OperatingSystem' without an extra class in the conceptual model.
            modelBuilder.Entity<SoftwareApplication>()
                .HasMany(s => s.AvailableOnOperationSystems)
                .WithMany(os => os.SoftwareApplications);
        } // DeclareRelationsForSoftwareApplication.
        
        private void DeclareRelationsForOperatingSystem(ModelBuilder modelBuilder)
        {
            // Many-to-many relation between 'OperatingSystem' and 'SoftwareApplication' without an extra class in the conceptual model.
            modelBuilder.Entity<OperatingSystem>()
                .HasMany(os => os.SoftwareApplications)
                .WithMany(s => s.AvailableOnOperationSystems);
        } // DeclareRelationsForOperatingSystem.
        
        #endregion
    }
}



























