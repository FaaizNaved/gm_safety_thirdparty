using gm_safety_thirdparty.Data;
using gm_safety_thirdparty.Models;
using Microsoft.EntityFrameworkCore;

namespace gm_safety_thirdparty.Repositories
{
    public class ThirdPartyRepository : IThirdPartyRepository
    {
        private readonly ApplicationDbContext _context;

        public ThirdPartyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ThirdPartyVendor> GetAll() => _context.ThirdPartyVendors.ToList();

        public ThirdPartyVendor GetById(int id) => _context.ThirdPartyVendors.Find(id);

        public void Add(ThirdPartyVendor vendor)
        {
            _context.ThirdPartyVendors.Add(vendor);
            _context.SaveChanges();
        }

        public void Update(ThirdPartyVendor vendor)
        {
            _context.Entry(vendor).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var vendor = _context.ThirdPartyVendors.Find(id);
            if (vendor != null)
            {
                _context.ThirdPartyVendors.Remove(vendor);
                _context.SaveChanges();
            }
        }
    }
}
