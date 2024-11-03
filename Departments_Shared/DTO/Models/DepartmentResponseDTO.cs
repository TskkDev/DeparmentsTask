using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Models
{
    public record DepartmentResponseDTO
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required int Order { get; set; }
        public int? ParentId { get; set; }
    }
}
