using Departments_DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departmens_DAL.Entity
{
    public record Departments : BaseEntity
    {
        public required string Name { get; set; }
        public int Order { get; set; }  
        public int? ParentId { get; set; }  
        public virtual Departments Parent { get; set; }
        public virtual IEnumerable<Departments> SubDepartments { get; set; } = new List<Departments>();
    }
}
