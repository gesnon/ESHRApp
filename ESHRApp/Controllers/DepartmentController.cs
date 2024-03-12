using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Departments;
using ESHRApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESHRApp.Controllers
{
    public class DepartmentController: BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService _departmentService)
        {
            this._departmentService = _departmentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] DepartmentCreateDTO dto)
        {
            return Ok(await _departmentService.CreateAsync(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await _departmentService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery]string name)
        {
            return Ok(await _departmentService.GetAsync(name));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] DepartmentCreateDTO dto, int id)
        {
            return Ok(await _departmentService.UpdateAsync(dto, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return Ok(await _departmentService.DeleteAsync( id));
        }
                
       
    }
}
