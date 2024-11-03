using Departmens_DAL.Entity;
using Departments_BLL.Interfaces;
using Departments_DAL.Interfaces;
using Departments_Shared.DTO.Request;
using Departments_Shared.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Departments_BLL.Services
{
    public sealed class DepartamentsService : IDepartmentsService
    {
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartamentsService(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<GetDepartmentResponse> GetDepartment(GetDepartmentRequest request)
        {
            var department = await _departmentsRepository.GetDepartment(request.Id);
            return new()
            {
                Department = department.ToModel()
            };
        }

        public async Task<GetDepartmentsResponse> GetDepartments()
        {
            var departments = await _departmentsRepository.GetDepartments();
            return new()
            {
                Department = departments.Select(x => x.ToModel())
            };
        }
        public async Task DeleteDepartment(DeleteDepartmentRequest request)
        {
            var department = await _departmentsRepository.GetDepartment(request.Id);
            await _departmentsRepository.DeleteDepartment(department);
        }
        public async Task<CreateDepartmentResponse> CreateDepartment(CreateDepartmentRequest request)
        {
            var department = await _departmentsRepository.InsertDepartment(request.Data.ToEntity());
            return new() 
            { 
                Department = department.ToModel() 
            };
        }

        public async Task<UpdateDepartmentResponse> UpdateDepartment(UpdateDepartmentRequest request)
        {
            var department = await _departmentsRepository.GetDepartment(request.Id);
            if (request.Id == request.Data.ParentId)
                throw new HttpRequestException("Same ID and ParentID");
            if(request.Data.ParentId != null)
            {
                if(request.Data.ParentId != 0)
                {
                    var parentDepartment = await _departmentsRepository.GetDepartment((int)request.Data.ParentId);
                    if(parentDepartment.ParentId == request.Id)
                        throw new HttpRequestException("Invalid Id and ParentID, warning recycle");
                }
                department = await _departmentsRepository.UpdateDepartment(department, request.Data.ParentId);

            }
            return new()
            {
                Department = department.ToModel()
            };
        }

        //возможно необходимо будет синхронизровать потоки.
        public async Task<string> ExportDepartmentsToXml()
        {
            var departments = await _departmentsRepository.GetAllDepartments();

            var serializer = new XmlSerializer(typeof(List<Departments>));
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Exports");
            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, $"Departments_{DateTime.Now:yyyyMMdd_HHmmss}.xml");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, departments.ToList());
            }

            return filePath;
        }
        public async Task ImportDepartmentsFromXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new FileLoadException("Invalid file");

            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var serializer = new XmlSerializer(typeof(List<Departments>));
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var departments = (List<Departments>)serializer.Deserialize(stream);
                foreach(var department in departments)
                {
                    await _departmentsRepository.InsertDepartment(department);
                }
            }

        }
    }
}
