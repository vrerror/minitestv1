using DataAccess.Enities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CourseContext : IdentityDbContext<ApplicationUser> 
    {
        public CourseContext(DbContextOptions options) : base(options)
        {
        }
        
        public virtual DbSet<FurnitureCategory> FurnitureCategory { get; set; }
        public virtual DbSet<FurnitureProduct> FurnitureProduct { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }



    }
}
