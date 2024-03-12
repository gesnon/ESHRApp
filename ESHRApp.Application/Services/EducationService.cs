using AutoMapper;
using ESHRApp.Application.Interfaces;
using ESHRApp.Application.Models.Educations;
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
    public class EducationService : IEducationService
    {
        private readonly ESHRAppContext _context;
        

        public EducationService(
            ESHRAppContext context)
        {
            _context = context;
            
        }

        public async Task<int> CreateAsync(EducationCreateDTO dto)
        {
            Education education = new Education { Name = dto.Name };
            _context.Educations.Add(education);

            await _context.SaveChangesAsync(CancellationToken.None);
            return education.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Education education = await _context.Educations.FirstOrDefaultAsync(_ => _.Id == id);
            if (education == null)
            {
                throw new Exception("Образование не найдено");
            }
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync(CancellationToken.None);
            return education.Id;
        }

        public async Task<EducationGetDTO> GetByIdAsync(int id)
        {
            Education education = await _context.Educations.FirstOrDefaultAsync(_ => _.Id == id);
            if (education == null)
            {
                throw new Exception("Образование не найдено");
            }            

            return new EducationGetDTO
            {
                Id = education.Id,
                Name = education.Name
            };
        }

        public async Task<List<EducationGetDTO>> GetAsync(string? name)
        {
            var query = _context.Educations.AsQueryable();


            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            List<EducationGetDTO> result = await query.Select(_ => new EducationGetDTO
            {
                Name = _.Name,
                Id = _.Id
            }).ToListAsync();

            return result;
        }

        public async Task<int> UpdateAsync(EducationCreateDTO dto, int id)
        {
            Education education =  await _context.Educations.FirstOrDefaultAsync(_ => _.Id == id);
            if (education == null)
            {
                throw new Exception("Образование не найдено");
            }
            education.Name = dto.Name;
            await _context.SaveChangesAsync(CancellationToken.None);
            return education.Id;
        }
    }
}
