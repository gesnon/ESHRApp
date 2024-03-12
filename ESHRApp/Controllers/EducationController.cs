using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Educations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESHRApp.Controllers
{
    public class EducationController: BaseController
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService _educationService)
        {
            this._educationService = _educationService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] EducationCreateDTO dto)
        {
            return Ok(await _educationService.CreateAsync(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await _educationService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery]string name)
        {
            return Ok(await _educationService.GetAsync(name));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] EducationCreateDTO dto, int id)
        {
            return Ok(await _educationService.UpdateAsync(dto, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return Ok(await _educationService.DeleteAsync(id));
        }
    }
}
