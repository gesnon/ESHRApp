using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Promotions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ESHRApp.Controllers
{
    public class PromotionController : BaseController
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService _promotionService)
        {
            this._promotionService = _promotionService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] PromotionCreateDTO dto)
        {
            return Ok(await _promotionService.CreateAsync(dto));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return Ok(await _promotionService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            return Ok(await _promotionService.GetAll(startDate , endDate));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync([FromBody] PromotionCreateDTO dto, int id)
        {
            return Ok(await _promotionService.UpdateAsync(dto, id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return Ok(await _promotionService.DeleteAsync(id));
        }

        [HttpGet("report/promo")]
        public async Task<ActionResult> SavePromotionTable([FromQuery]DateTime startDate, [FromQuery] DateTime endDate)
        {
            MemoryStream ms = await _promotionService.GetPromotionTable(startDate, endDate);

            var arr = ms.ToArray();
            
            ms.Dispose();

            return File(arr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"Отчет за период { startDate} - {endDate}.xlsx");           
        }

        [HttpGet("report/no-promo")]
        public async Task<ActionResult> SaveUnluckEmployees([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            MemoryStream ms = await _promotionService.GetEmployeeWithNoPromotion(startDate, endDate);

            var arr = ms.ToArray();

            ms.Dispose();

            return File(arr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: $"Отчет за период { startDate} - {endDate}.xlsx");
        }

    }
}
