using PropertyManager.Models;

namespace PropertyManager.Repositories
{
    public interface IPropertyRepository
    {
        void Add(Property property);
        void Delete(int id);
        List<Property> GetAll();
        Property GetById(int id);
        void Update(Property property);
    }
}