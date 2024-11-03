using Departmens_DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Models
{
    public record DepartmentModel
    {
        public required int Id { get; init; }

        public required string Name { get; init; }

        public required int Order { get; init; }
        public int? ParentId { get; init; }
        public virtual IEnumerable<DepartmentModel> SubDepartments { get; init; } = new List<DepartmentModel>();
    }
}
