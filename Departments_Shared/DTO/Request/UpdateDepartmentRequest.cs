using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Request
{
    public record UpdateDepartmentRequest : BaseDepartmentRequest
    {
        [FromBody]
        public required UpdateDepartmentResponseData Data { get; init; } 
    }
    public record UpdateDepartmentResponseData 
    {
        public required int? ParentId { get; init; }
    }
}
