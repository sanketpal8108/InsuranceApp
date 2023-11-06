using InsuranceDay1.Models;

namespace InsuranceProject.Services
{
    public interface ICustomerService
    {
        public List<Customer> GetAll();
        public Customer Get(int id);

        public Customer Check(int id);
        public int Add(Customer customer);
        public Customer Update(Customer customer);
        public void Delete(Customer customer);
    }
}
