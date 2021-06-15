using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await this._context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            this._context.Add(obj);
            await this._context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await this._context.Seller.Include(obj => obj.Department)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                Seller obj = await this._context.Seller.FindAsync(id);
                this._context.Seller.Remove(obj);
                await this._context.SaveChangesAsync();
            }
            catch(DbUpdateException err)
            {
                throw new IntegrityException(err.Message);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await this._context.Seller.AnyAsync(x => x.Id == obj.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not founded!");
            }

            try
            {
                this._context.Update(obj);
                await this._context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
