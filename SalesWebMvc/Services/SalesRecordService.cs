using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> result = from obj in this._context.SalesRecord select obj;
            if(minDate.HasValue)
            {
                result = result.Where(d => d.Date >= minDate.Value);
            }

            if(maxDate.HasValue)
            {
                result = result.Where(d => d.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> result = from obj in this._context.SalesRecord select obj;
            if(minDate.HasValue)
            {
                result = result.Where(d => d.Date >= minDate.Value);
            }

            if(maxDate.HasValue)
            {
                result = result.Where(d => d.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
