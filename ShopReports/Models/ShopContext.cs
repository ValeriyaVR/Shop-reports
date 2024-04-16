using Microsoft.EntityFrameworkCore;

namespace ShopReports.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasMany(e => e.Titles)
            .WithOne(e => e.Category)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<ProductTitle>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Title)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.TitleId);

            modelBuilder.Entity<Product>()
            .HasMany(e => e.OrderDetails)
            .WithOne(e => e.Product)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Supplier>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Supplier)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.SupplierId);

            modelBuilder.Entity<Manufacturer>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Manufacturer)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.ManufacturerId);

            modelBuilder.Entity<City>()
            .HasMany(e => e.Locations)
            .WithOne(e => e.City)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.CityId);

            modelBuilder.Entity<Location>()
            .HasMany(e => e.Supermarkets)
            .WithOne(e => e.Location)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Supermarket>()
            .HasMany(e => e.Locations)
            .WithOne(e => e.Supermarket)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.SupermarketId);

            modelBuilder.Entity<SupermarketLocation>()
            .HasMany(e => e.Orders)
            .WithOne(e => e.SupermarketLocation)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.SupermarketLocationId);

            modelBuilder.Entity<Person>()
            .HasMany(e => e.Contacts)
            .WithOne(e => e.Person)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Person>()
            .HasOne(e => e.Customer)
            .WithOne(e => e.Person)
            .HasForeignKey<Person>(e => e.Id);

            modelBuilder.Entity<PersonContact>()
            .HasOne(e => e.Type)
            .WithMany(e => e.Contacts)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.ContactTypeId);

            modelBuilder.Entity<ContactType>()
            .HasMany(e => e.Contacts)
            .WithOne(e => e.Type)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.ContactTypeId);

            modelBuilder.Entity<Customer>()
            .HasMany(e => e.Orders)
            .WithOne(e => e.Customer)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.CustomerId)
            .IsRequired(false);

            modelBuilder.Entity<Order>()
            .HasMany(e => e.Details)
            .WithOne(e => e.Order)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.OrderId);
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductTitle> Titles { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Supermarket> Supermarkets { get; set; }

        public DbSet<SupermarketLocation> SupermarketLocations { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PersonContact> PersonContacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
