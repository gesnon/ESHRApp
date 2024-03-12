using ESHRApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Models.Employees
{
    public class EmployeeGetDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PersonalNumber { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public int DepartmentId { get; set; }
        public string Education { get; set; }
        public int EducationId { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime DateOfLeave { get; set; }
    }
}
