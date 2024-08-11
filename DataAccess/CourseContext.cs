using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    
    public class CourseContext : IdentityDbContext<ApplicationUser>  
    {
        public CourseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CategoryET> CategoryET { get; set; }
        public DbSet<Product> Product { get; set; }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
