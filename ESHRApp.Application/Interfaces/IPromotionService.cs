using ESHRApp.Application.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESHRApp.Application.Interfaces
{
    public interface IPromotionService
    {
        public Task<int> CreateAsync(PromotionCreateDTO dto);
        public Task<int> UpdateAsync(PromotionCreateDTO dto, int id);
        public Task<PromotionGetDTO> GetAsync(int id);
        public Task<List<PromotionGetDTO>> GetAll(DateTime startDate, DateTime endDate);
        public Task<int> DeleteAsync(int id);
        public Task<MemoryStream> GetPromotionTable(DateTime startDate, DateTime endDate);
        public Task<MemoryStream> GetEmployeeWithNoPromotion(DateTime startDate, DateTime endDate);
        
    }
}
