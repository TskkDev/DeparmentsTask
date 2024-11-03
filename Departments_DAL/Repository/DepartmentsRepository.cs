
using Departmens_DAL;
using Departmens_DAL.Entity;
using Departments_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Departments_DAL.Repository
{
    public sealed class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly DepartmentsContext _context;
        public DepartmentsRepository(DepartmentsContext context)
        {
            _context = context;
        }
        public async Task<Departments> GetDepartment(int id)
        {
            var department = await _context.Departments.Include(d => d.SubDepartments).FirstOrDefaultAsync(x=> x.Id ==id);
            if (department == null)
                throw new InvalidDataException("Invalid ID");
            return department;
        }

        // предусматриваю такую логку отображения: изначально отдаем для построения дерева все департменты которые не имеют родителей 
        // после этого при раскрытии списка дочерних департментов отправляет запрос в GetDepartment и отдаем информацию о дочерних департментах

        // почему не отдача общим массивом? - при большом колличестве департментов будет значительно проседать по перфомансу
        public async Task<IEnumerable<Departments>> GetDepartments()
        {
            var departments = await _context.Departments.Where(d => d.ParentId == null).ToListAsync();
            if (!departments.Any())
                throw new NullReferenceException("No departments in DB");
            return departments;
        }
        public async Task DeleteDepartment(Departments department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Departments> InsertDepartment(Departments department)
        {
            department.Id = 0;
            department.Order = await _context.Departments.CountAsync(d => d.ParentId == department.ParentId) + 1;
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Departments> UpdateDepartment(Departments department, int? newParentId)
        {
            department.ParentId = newParentId == 0
                ? null
                : newParentId;
            department.Order = await _context.Departments.CountAsync(d => d.ParentId == department.ParentId) + 1;
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task<IEnumerable<Departments>> GetAllDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            if (!departments.Any())
                throw new NullReferenceException("No departments in DB");
            return departments;
        }
    }
}
