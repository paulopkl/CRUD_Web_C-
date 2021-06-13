using SalesWebMvc.Data;
using SalesWebMvc.Models;
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

        public List<Seller> FindAll()
        {
            return this._context.Seller.ToList<Seller>();
        }

        public void Insert(Seller obj)
        {
            this._context.Add(obj);
            this._context.SaveChanges();
        }
    }
}
