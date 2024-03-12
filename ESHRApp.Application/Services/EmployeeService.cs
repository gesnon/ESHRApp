using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Employees;
using ESHRApp.Domain.Entities;
using ESHRApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ESHRAppContext _context;


        public EmployeeService(
            ESHRAppContext context)
        {
            _context = context;

        }
        public async Task<int> CreateAsync(EmployeeCreateDTO dto)
        {
            Employee employee = new Employee
            {

                DateOfBirth = dto.DateOfBirth,
                DateOfEmployment = dto.DateOfEmployment,
                //DateOfLeave = dto.DateOfLeave, 
                //Department = dto.Department,
                DepartmentId = dto.DepartmentId,
                //Education = dto.Education,
                EducationId = dto.EducationId,
                FullName = dto.FullName,
                PersonalNumber = dto.PersonalNumber,
                Sex = dto.Sex
            };
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync(CancellationToken.None);
            return employee.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(_ => _.Id == id);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден");
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(CancellationToken.None);
            return employee.Id;
        }

        public async Task<EmployeeGetDTO> GetByIdAsync(int id)
        {
            Employee employee = await _context.Employees
                .Include(_ => _.Department)
                .Include(_ => _.Education)
                .FirstOrDefaultAsync(_ => _.Id == id);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден");
            }

            return new EmployeeGetDTO
            {
                Id = employee.Id,
                DateOfBirth = employee.DateOfBirth,
                DateOfEmployment = employee.DateOfEmployment,
                DateOfLeave = employee.DateOfLeave,
                Department = employee.Department.Name,
                DepartmentId = employee.DepartmentId,
                Education = employee.Education.Name,
                EducationId = employee.EducationId,
                FullName = employee.FullName, 
                PersonalNumber = employee.PersonalNumber,
                Sex = employee.Sex

            };
        }

        public async Task<List<EmployeeGetDTO>> GetAsync(string seachValue, string sortOrder, string sortField)
        {
            var query = _context.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(seachValue) )
            {
                query = query.Where(e => e.FullName.ToLower().Contains(seachValue) ||
                e.PersonalNumber.ToLower().Contains(seachValue));
            }

            if(sortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(GetSortField(sortField));
            }
            else
            {
                query = query.OrderBy(GetSortField(sortField));
            }

            List<EmployeeGetDTO> result = await query.Select(dto => new EmployeeGetDTO
            {
                Id = dto.Id,
                DateOfBirth = dto.DateOfBirth,
                DateOfEmployment = dto.DateOfEmployment,
                DateOfLeave = dto.DateOfLeave,
                Department = dto.Department.Name,
                DepartmentId = dto.DepartmentId,
                Education = dto.Education.Name,
                EducationId = dto.EducationId,
                FullName = dto.FullName,
                PersonalNumber = dto.PersonalNumber,
                Sex = dto.Sex
            }).ToListAsync();

            return result;
        }

        public async Task<int> KickAsync(int id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(_ => _.Id == id);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден");
            }
            employee.DateOfLeave = DateTime.Now;
            await _context.SaveChangesAsync(CancellationToken.None);
            return employee.Id;
        }

        public async Task<int> UpdateAsync(EmployeeCreateDTO dto, int id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(_ => _.Id == id);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден");
            }
            employee.DateOfBirth = dto.DateOfBirth;
            employee.DateOfEmployment = dto.DateOfEmployment;
            employee.DateOfLeave = dto.DateOfLeave;
            //.Department = dto.Department;
            employee.DepartmentId = dto.DepartmentId;
            //.Education = dto.Education;
            employee.EducationId = dto.EducationId;
            employee.FullName = dto.FullName;
            employee.PersonalNumber = dto.PersonalNumber;
            employee.Sex = dto.Sex;
            await _context.SaveChangesAsync(CancellationToken.None);
            return employee.Id;
        }

        private static Expression<Func<Employee, object>> GetSearchField(string searchField)
        {
            return searchField?.ToLower() switch
            {
                "fullname" => employee => employee.FullName,
                "personalnumber" => employee => employee.PersonalNumber,
                _ => employee => employee.FullName
            };
        }

        private static Expression<Func<Employee, object>> GetSortField(string sortField)
        {
            return sortField?.ToLower() switch
            {
                "fullname" => employee => employee.FullName,
                "personalnumber" => employee => employee.PersonalNumber,
                "dateofbirth" => employee => employee.DateOfBirth,
                "dateofemployment" => employee => employee.DateOfEmployment,
                "dateofleave" => employee => employee.DateOfLeave,
                "department" => employee => employee.Department.Name,
                "id" => employee => employee.Id,
                _ => employee => employee.FullName
            };
        }

        private Expression<Func<Employee, bool>> GetFilter(string fieldName, string fieldValue)
        {
            if(fieldName == "department")
            {
                fieldName = "Department.Name";
            }
            ParameterExpression parameter = Expression.Parameter(typeof(Employee), "x");
            Expression property = Expression.Property(parameter, fieldName);
            Expression target = Expression.Constant(fieldValue);
            Expression containsMethod = Expression.Call(property, "Contains", null, target);
            return Expression.Lambda<Func<Employee, bool>>(containsMethod, parameter);
        }
    }
}
