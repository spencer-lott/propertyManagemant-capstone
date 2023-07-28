using PropertyManager.Models;

namespace PropertyManager.Repositories
{
    public interface ITenantRepository
    {
        void Add(Tenant tenant);
        void Delete(int id);
        List<Tenant> GetAll();
        Tenant GetById(int id);
        void Update(Tenant tenant);
    }
}