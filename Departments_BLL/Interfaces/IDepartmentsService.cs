using Departments_Shared.DTO.Request;
using Microsoft.AspNetCore.Http;

namespace Departments_BLL.Interfaces
{
    public interface IDepartmentsService
    {
        Task<GetDepartmentsResponse> GetDepartments();
        Task<GetDepartmentResponse> GetDepartment(GetDepartmentRequest request);
        Task<CreateDepartmentResponse> CreateDepartment(CreateDepartmentRequest request);
        Task DeleteDepartment(DeleteDepartmentRequest request);
        Task<UpdateDepartmentResponse> UpdateDepartment(UpdateDepartmentRequest request);
        Task<string> ExportDepartmentsToXml();
        Task ImportDepartmentsFromXml(IFormFile file);
    }
}
