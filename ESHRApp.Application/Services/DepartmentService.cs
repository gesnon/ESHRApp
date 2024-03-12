using AutoMapper;
using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Departments;
using ESHRApp.Domain.Entities;
using ESHRApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ESHRAppContext _context;

        public DepartmentService(
            ESHRAppContext context)
        {
            _context = context;

        }
        public async Task<int> CreateAsync(DepartmentCreateDTO dto)
        {
            Department department = new Department { Name = dto.Name };
            _context.Departments.Add(department);

            await _context.SaveChangesAsync(CancellationToken.None);
            return department.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Department department = await _context.Departments.FirstOrDefaultAsync(_ => _.Id == id);
            if (department == null)
            {
                throw new Exception("Подразденение не найдено");
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(CancellationToken.None);
            return department.Id;
        }

        public async Task<DepartmentGetDTO> GetByIdAsync(int id)
        {
            Department department = await _context.Departments.FirstOrDefaultAsync(_ => _.Id == id);
            if (department == null)
            {
                throw new Exception("Подразденение не найдено");
            }

            return new DepartmentGetDTO
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public async Task<List<DepartmentGetDTO>> GetAsync(string? name)
        {
            var query = _context.Departments.AsQueryable();


            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            List<DepartmentGetDTO> result = await query.Select(_ => new DepartmentGetDTO
            {
                Id = _.Id,
                Name = _.Name
            }).ToListAsync();

            return result;
        }

        public async Task<int> UpdateAsync(DepartmentCreateDTO dto, int id)
        {
            Department department = await _context.Departments.FirstOrDefaultAsync(_ => _.Id == id);
            if (department == null)
            {
                throw new Exception("Подразденение не найдено");
            }
            department.Name = dto.Name;
            await _context.SaveChangesAsync(CancellationToken.None);
            return department.Id;
        }
    }
}
