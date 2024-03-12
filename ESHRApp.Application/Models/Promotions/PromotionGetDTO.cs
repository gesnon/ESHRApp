using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Models.Promotions
{
    public class PromotionGetDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePersonalNumber { get; set; }
        public decimal IncreasePercentage { get; set; }
        public DateTime PromotionDate { get; set; }
    }
}
