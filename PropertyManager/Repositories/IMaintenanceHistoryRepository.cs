using PropertyManager.Models;

namespace PropertyManager.Repositories
{
    public interface IMaintenanceHistoryRepository
    {
        void Add(MaintenanceHistory note);
        void Delete(int id);
        List<MaintenanceHistory> GetAll();
        MaintenanceHistory GetById(int id);
        void Update(MaintenanceHistory note);
    }
}