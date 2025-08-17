using gm_safety_thirdparty.Models;

namespace gm_safety_thirdparty.Repositories
{
    public interface IThirdPartyRepository
    {
        IEnumerable<ThirdPartyVendor> GetAll();
        ThirdPartyVendor GetById(int id);
        void Add(ThirdPartyVendor vendor);
        void Update(ThirdPartyVendor vendor);
        void Delete(int id);
    }
}
