using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Request
{
    public record CreateDepartmentRequest
    {
        [FromBody]
        public required CreateDepartmentRequestData Data { get; init; }
    }

    public record CreateDepartmentRequestData
    {
        public required string Name { get; init; }
        public int? ParentId { get; init; }
    }
}
