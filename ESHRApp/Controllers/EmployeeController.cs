using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESHRApp.Controllers
{
    public class EmployeeController: BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            this._employeeService = _employeeService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] EmployeeCreateDTO dto)
        {
            return Ok(await _employeeService.CreateAsync(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await _employeeService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] string seachValue, [FromQuery] string sortOrder, [FromQuery] string sortField)
        {
            return Ok(await _employeeService.GetAsync(seachValue, sortOrder, sortField));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] EmployeeCreateDTO dto, int id)
        {
            return Ok(await _employeeService.UpdateAsync(dto, id));
        }

        [HttpPut("{id}/kick")]
        public async Task<ActionResult> KickEmployee(int id)
        {
            return Ok(await _employeeService.KickAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return Ok(await _employeeService.DeleteAsync(id));
        }
    }
}
