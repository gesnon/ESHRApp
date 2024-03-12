using ESHRApp.Application.Models.Educations;
using ESHRApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Interfaces
{
    public interface IEducationService
    {
        public Task<int> CreateAsync(EducationCreateDTO dto);
        public Task<int> UpdateAsync(EducationCreateDTO dto, int id);
        public Task<EducationGetDTO> GetByIdAsync(int id);
        public Task<List<EducationGetDTO>> GetAsync(string? name);
        public Task<int> DeleteAsync(int id);
    }
}
