﻿using Departments_Shared.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.DTO.Request
{
    public record CreateDepartmentResponse
    {
        public required DepartmentModel Department { get; init; }
    }
}
