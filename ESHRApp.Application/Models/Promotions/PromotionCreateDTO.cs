using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Models.Promotions
{
    public class PromotionCreateDTO
    {
        public int EmployeeId { get; set; }
        public decimal IncreasePercentage { get; set; }
        public DateTime PromotionDate { get; set; }
    }
}
