using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Request
{
    public class CreateDepartmentResponse
    {
        public required string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
