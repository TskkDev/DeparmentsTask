using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Models
{
    public record DepartmentRequestDTO
    {
        public required int Id { get; set; }
        // лучше разделить на 2 модельки, но так как в предоставленном задание имеем в качестве операции update только смену родительской категории,
        // такой реализации будет достаточно, при расширении изменть.
        public int? ParentId { get; set; }
    }
}
