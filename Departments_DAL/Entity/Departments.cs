using Departments_DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Departmens_DAL.Entity
{
    // использую в этом месте место рекордов класс, чтобы облегчить в дальнейшем обновление полей при вызове update,
    //upd плюс XmlSerializer не дружит с рекордами до той степени что крашит апи 
    public class Departments : BaseEntity
    {
        public required string Name { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        [XmlIgnore]
        public virtual Departments? Parent { get; set; }
        [XmlIgnore]
        public virtual IEnumerable<Departments> SubDepartments { get; set; } = new List<Departments>();
    }
}
