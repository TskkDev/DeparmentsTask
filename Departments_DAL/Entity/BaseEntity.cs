using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_DAL.Entity
{
    public record BaseEntity
    {
        public required int Id { get; set; }
    }
}
