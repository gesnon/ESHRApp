using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Domain.Entities
{
    public class Employee : Entity
    {
        public string FullName { get; set; }
        public string PersonalNumber { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public Education Education { get; set; }
        public int EducationId { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime DateOfLeave { get; set; }
    }
}
