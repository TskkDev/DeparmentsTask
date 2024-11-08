﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Departments_Shared.DTO.Request
{
    public record BaseDepartmentRequest
    {
        [FromRoute]
        public required int Id { get; init; }
    }
}
