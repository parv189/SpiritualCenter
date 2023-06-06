using Microsoft.EntityFrameworkCore;

namespace spiritual_center.Models
{
    public class UserContext : DbContext
    {
        public UserContext()
        {

        }
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Payments> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=PC0404\\MSSQL2019;Database=SpiritualCenterDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
