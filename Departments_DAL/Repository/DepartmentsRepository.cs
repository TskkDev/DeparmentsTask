
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
            => await _context.Departments.Where(d => d.ParentId == null).ToListAsync();
        public async Task DeleteDepartment(int id)
        {
            var department = await GetDepartment(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Departments> InsertDepartment(Departments department)
        {
            department.Order = await _context.Departments.CountAsync(d => d.ParentId == department.ParentId) + 1;
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Departments> UpdateDepartment(int id, int? newParentId)
        {
            var department = await GetDepartment(id);

            var newParentDepartment = await GetDepartment(id);

            department.ParentId = newParentId;
            await _context.SaveChangesAsync();
            return department;
        }
    }
}
