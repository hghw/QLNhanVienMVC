using Microsoft.EntityFrameworkCore;
namespace MVCExample.Models
{
    public class nhan_vienContext : DbContext
    {
        public DbSet<nhan_vien> nhan_viens { set; get; }

        private const string connectionString = @"Server=localhost;Port=5433;User Id = postgres; Password=123;Database=NhanVienDB;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}