using Departmens_DAL.Entity;


namespace Departments_DAL.Interfaces
{
    public interface IDepartmentsRepository
    {
        Task<IEnumerable<Departments>> GetDepartments();
        Task<Departments> GetDepartment(int id);
        Task<Departments> InsertDepartment(Departments department);
        Task DeleteDepartment(int id);
        Task<Departments> UpdateDepartment(int id, int? newParentId);
    }
}
