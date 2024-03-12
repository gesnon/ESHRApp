
using ESHRApp.Application.Models.Departments;
using ESHRApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Interfaces
{
    public interface IDepartmentService
    {
        public Task<int> CreateAsync(DepartmentCreateDTO dto);
        public Task<int> UpdateAsync(DepartmentCreateDTO dto, int id);
        public Task<DepartmentGetDTO> GetByIdAsync(int id);
        public Task<List<DepartmentGetDTO>> GetAsync(string? name);
        public Task<int> DeleteAsync(int id);
    }
}
