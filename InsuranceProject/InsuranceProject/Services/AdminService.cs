using InsuranceDay1.Models;
using InsuranceProject.Repository;

namespace InsuranceProject.Services
{
    public class AdminService:IAdminService
    {
        private IEntityRepository<Admin> _entityRepository;

        public AdminService(IEntityRepository<Admin> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public List<Admin> GetAll()
        {
            var adminQuery = _entityRepository.Get();
            var admins=adminQuery.Where(admin=>admin.IsActive).ToList();
            return admins;
        }

        public Admin Get(int id)
        {
            var adminQuery= _entityRepository.Get();
            var admin=adminQuery.Where(admin=>admin.Id==id).FirstOrDefault();
            return admin;
        }

        public Admin Check(int id)
        {
            return _entityRepository.Get(id);
        }

        public int Add(Admin admin)
        {
            return _entityRepository.Add(admin);
        }

        public Admin Update(Admin admin)
        {
            return _entityRepository.Update(admin);
        }

        public void Delete(Admin admin)
        {
            _entityRepository.Delete(admin);
        }
    }
}
