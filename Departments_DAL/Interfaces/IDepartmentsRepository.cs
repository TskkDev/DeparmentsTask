using Departmens_DAL.Entity;


namespace Departments_DAL.Interfaces
{
    public interface IDepartmentsRepository
    {
        Task<IEnumerable<Departments>> GetDepartments();
        Task<IEnumerable<Departments>> GetAllDepartments();
        Task<Departments> GetDepartment(int id);
        Task<Departments> InsertDepartment(Departments department);
        Task DeleteDepartment(Departments department);
        Task<Departments> UpdateDepartment(Departments department, int? newParentId);
    }
}
