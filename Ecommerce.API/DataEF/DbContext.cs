using Ecommerce.API.DataEF.EntityMappings;
using Ecommerce.API.DataEF.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.DataEF.Context
{
    /// <summary>
    /// Db context
    /// </summary>
    public class EcommerceContext(DbContextOptions<EcommerceContext> options) : DbContext(options)
    {
        /// <summary>
        /// Products db set
        /// </summary>
        public DbSet<ProductModel> Products => Set<ProductModel>();


        /// <summary>
        /// Map entities config
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
        }
    }
}
