using Departments_Shared.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Request
{
    public class GetDepartmentsResponse
    {
        public required IEnumerable<DepartmentModel> Department { get; init; }
    }
}
