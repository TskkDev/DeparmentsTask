using Departmens_DAL.Entity;
using Departments_Shared.DTO.Models;
using Departments_Shared.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_Shared.Mapper
{
    public static class MappingExtensions
    {
        public static DepartmentModel ToModel(this Departments department)
        {
            if (department == null) return null;

            return new DepartmentModel
            {
                Id = department.Id,
                Name = department.Name,
                Order = department.Order,
                ParentId = department.ParentId,
                SubDepartments = department.SubDepartments.Select(x=>new DepartmentModel()
                {
                    Id=x.Id,
                    Name=x.Name,
                    Order=x.Order,
                    ParentId=x.ParentId,
                })
            };
        }

        public static Departments ToEntity(this CreateDepartmentRequestData data)
        {
            if (data == null) return null;

            return new Departments
            {
                Name = data.Name,
                ParentId = data.ParentId == 0 
                ? null
                : data.ParentId,
            };
        }
    }
}
