using Departmens_DAL.Entity;
using Departments_BLL.Interfaces;
using Departments_Shared.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deparments_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentService;

        public DepartmentsController(IDepartmentsService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<GetDepartmentsResponse>> GetDepartments()
        {
            try
            {
                var departments = await _departmentService.GetDepartments();
                return Ok(departments);
            }
            catch (NullReferenceException ex) 
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<GetDepartmentResponse>> GetDepartments(GetDepartmentRequest request)
        {
            try
            {
                var departments = await _departmentService.GetDepartment(request);
                return Ok(departments);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateDepartmentResponse>> PostDepartment(CreateDepartmentRequest request)
        {
            var response = await _departmentService.CreateDepartment(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDepartment(DeleteDepartmentRequest request)
        {
            try
            {
                await _departmentService.DeleteDepartment(request);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<UpdateDepartmentResponse>> UpdateDepartment(UpdateDepartmentRequest request)
        {
            try
            {
                var newDepartment = await _departmentService.UpdateDepartment(request);
                return Ok(newDepartment);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
               return BadRequest(ex.Message);
            }
        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportDepartments()
        {
            try
            {
                var filePath = await _departmentService.ExportDepartmentsToXml();
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var fileName = Path.GetFileName(filePath);

                return File(fileBytes, "application/xml", fileName);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportDepartments(IFormFile file)
        {
            try
            {
                await _departmentService.ImportDepartmentsFromXml(file);
                return Ok();
            }
            catch(FileLoadException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

