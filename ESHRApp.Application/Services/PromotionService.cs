using ClosedXML.Excel;
using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Employees;
using ESHRApp.Application.Models.Promotions;
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
    public class PromotionService : IPromotionService
    {
        private readonly ESHRAppContext _context;

        public PromotionService(
            ESHRAppContext context)
        {
            _context = context;

        }
        public async Task<int> CreateAsync(PromotionCreateDTO dto)
        {
            Promotion promotion = new Promotion
            {
                EmployeeId = dto.EmployeeId,
                IncreasePercentage = dto.IncreasePercentage,
                PromotionDate = dto.PromotionDate
            };
            _context.Promotions.Add(promotion);

            await _context.SaveChangesAsync(CancellationToken.None);
            return promotion.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Promotion promotion = await _context.Promotions.FirstOrDefaultAsync(_ => _.Id == id);
            if (promotion == null)
            {
                throw new Exception("Повышение не найдено");
            }
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync(CancellationToken.None);
            return promotion.Id;
        }

        public async Task<PromotionGetDTO> GetAsync(int id)
        {
            Promotion promotion = await _context.Promotions.Include(_ => _.Employee).FirstOrDefaultAsync(_ => _.Id == id);
            if (promotion == null)
            {
                throw new Exception("Повышение не найдено");
            }

            return new PromotionGetDTO
            {
                Id = promotion.Id,
                EmployeeId = promotion.EmployeeId,
                EmployeeName = promotion.Employee.FullName,
                IncreasePercentage = promotion.IncreasePercentage,
                PromotionDate = promotion.PromotionDate
            };
        }

        public async Task<List<PromotionGetDTO>> GetAll(DateTime startDate, DateTime endDate)
        {
            List<PromotionGetDTO> result = await _context.Promotions.Include(_ => _.Employee)
                .Where(_ => _.PromotionDate > startDate && _.PromotionDate < endDate)
                .Select(_ => new PromotionGetDTO
                {
                    Id = _.Id,
                    EmployeeId = _.EmployeeId,
                    EmployeeName = _.Employee.FullName,
                    EmployeePersonalNumber = _.Employee.PersonalNumber,
                    IncreasePercentage = _.IncreasePercentage,
                    PromotionDate = _.PromotionDate
                }).OrderBy(x => x.IncreasePercentage).ToListAsync();

            return result;
        }

        public async Task<int> UpdateAsync(PromotionCreateDTO dto, int id)
        {
            Promotion promotion = await _context.Promotions.FirstOrDefaultAsync(_ => _.Id == id);
            if (promotion == null)
            {
                throw new Exception("Повышение не найдено");
            }
            promotion.EmployeeId = dto.EmployeeId;
            promotion.IncreasePercentage = dto.IncreasePercentage;
            promotion.PromotionDate = dto.PromotionDate;
            await _context.SaveChangesAsync(CancellationToken.None);
            return promotion.Id;
        }

        public async Task<MemoryStream> GetPromotionTable(DateTime startDate, DateTime endDate)
        {
            List<PromotionGetDTO> promotions = await GetAll(startDate, endDate);

            MemoryStream stream = new MemoryStream();

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Отчет");

            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "ФИО сотрудника";
            ws.Cell(1, 3).Value = "Табельный номер";
            ws.Cell(1, 4).Value = "Процент повышения";
            ws.Cell(1, 5).Value = "Дата повышения";

            for (int i = 0; i < promotions.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = promotions[i].Id;
                ws.Cell(i + 2, 2).Value = promotions[i].EmployeeName;
                ws.Cell(i + 2, 3).Value = promotions[i].EmployeePersonalNumber;
                ws.Cell(i + 2, 4).Value = promotions[i].IncreasePercentage;
                ws.Cell(i + 2, 5).Value = promotions[i].PromotionDate.ToString();
            }
            ws.Columns().AdjustToContents();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;

        }


        public async Task<MemoryStream> GetEmployeeWithNoPromotion(DateTime startDate, DateTime endDate)
        {
            List<int> Ids = (await GetAll(startDate, endDate)).Select(i => i.EmployeeId).ToList();

            List<Employee> employees = _context.Employees.Where(_ => !Ids.Contains(_.Id)).ToList();


            MemoryStream stream = new MemoryStream();

            var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Отчет");

            ws.Cell(1, 1).Value = "Id сотрудника";
            ws.Cell(1, 2).Value = "ФИО сотрудника";
            ws.Cell(1, 3).Value = "Табельный номер";

            for (int i = 0; i < employees.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = employees[i].Id;
                ws.Cell(i + 2, 2).Value = employees[i].FullName;
                ws.Cell(i + 2, 3).Value = employees[i].PersonalNumber;
            }
            ws.Columns().AdjustToContents();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
