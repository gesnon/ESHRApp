using ESHRApp.Application.Models.Employees;

namespace ESHRApp.Application.Interfaces
{
    public interface IEmployeeService
    {
        public Task<int> CreateAsync(EmployeeCreateDTO dto);
        public Task<int> UpdateAsync(EmployeeCreateDTO dto, int id);
        public Task<EmployeeGetDTO> GetByIdAsync(int id);
        public Task<List<EmployeeGetDTO>> GetAsync(string seachValue, string sortOrder, string sortField);
        public Task<int> DeleteAsync(int id);
        public Task<int> KickAsync(int id);
        
    }
}
