using Departmens_DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departmens_DAL
{
    public sealed class DepartmentsContext :DbContext
    {
        public DepartmentsContext(DbContextOptions<DepartmentsContext> options) : base(options) { }

        public DbSet<Departments> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>()
                .HasMany(d => d.SubDepartments)
                .WithOne(d => d.Parent)
                .HasForeignKey(d => d.ParentId);
        }
    }
}
