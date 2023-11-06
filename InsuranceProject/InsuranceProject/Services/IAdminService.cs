using InsuranceDay1.Models;

namespace InsuranceProject.Services
{
    public interface IAdminService
    {
        public List<Admin> GetAll();

        public Admin Get(int id);
        public Admin Check(int id);
        public int Add(Admin admin);

        public Admin Update(Admin admin);

        public void Delete(Admin admin);

    }
}
