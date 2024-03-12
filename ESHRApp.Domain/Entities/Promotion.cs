using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Domain.Entities
{
    public class Promotion : Entity
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public decimal IncreasePercentage { get; set; }
        public DateTime PromotionDate { get; set; }
    }
}
