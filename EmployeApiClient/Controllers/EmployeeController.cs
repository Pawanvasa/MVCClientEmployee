using EmployeeApiConsumer.Models;
using EmployeeApiConsumer.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    public class EmployeeController : Controller
    {
        readonly IEmployeeServices _employeeServices;
        readonly IDepartmentServices _departmentServices;

        public EmployeeController(IEmployeeServices employeeServices, IDepartmentServices departmentServices)
        {
            _employeeServices = employeeServices;
            _departmentServices = departmentServices;
        }

        public IActionResult EmployeeDashboard()
        {
            return View();
        }

        public async Task<JsonResult> GetAllDepartments()
        {
            var departments = await _departmentServices.GetDepartmentsAsync();
            return Json(departments);
        }


        public async Task<IActionResult> GetAllEmployees()
        {
            ViewBag.departments = await _departmentServices.GetDepartmentsAsync();
            var employees = await _employeeServices.GetEmployeesAsync();
            return Json(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.departments = await _departmentServices.GetDepartmentsAsync();
            return PartialView("CreateNew");
        }

        public async Task<JsonResult> CreateEmployee(Employee employee)
        {
            var result = await _employeeServices.AddEmployeeAsync(employee);
            return Json(employee);
        }


        public async Task<IActionResult> Edit(int id)
        {

            ViewBag.departments = await _departmentServices.GetDepartmentsAsync();
            var emp = await _employeeServices.GetEmployeeByIdAsync(id);
            return Json(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmp(Employee employee)
        {
            var respose = await _employeeServices.EditEmployeeAsync(employee);
            return Json(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var respose = await _employeeServices.DeletEmployeeAsync(id);
            return Json("Record Deleted Successfully");
        }

        public async Task<IActionResult> PatchEmployee([FromBody] JsonPatchDocument patchDocument, int id)
        {
            var response = await _employeeServices.PatchEmployee(patchDocument, id);
            return Json("Record Updated Successfully");
        }
    }
}